﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroRedo.Content;
using RetroRedo.Input;
using RetroRedo.Maps;

namespace RetroRedo.Screen
{
    public class GameScreen : IScreen
    {
        private readonly IContentChest _contentChest;
        private readonly IGameStateService _gameStateService;
        private readonly IMapLoader _mapLoader;
        private readonly IMapRenderer _mapRenderer;
        private readonly IInputService _inputService;

        private Map _activeMap;

        public ScreenType ScreenType => ScreenType.Game;
        public bool Ended { get; private set; }
        public Action<ScreenType> RequestScreenChange { get; set; }

        public GameScreen(IContentChest contentChest, IGameStateService gameStateService, IMapLoader mapLoader,
            IMapRenderer mapRenderer, IInputService inputService)
        {
            _contentChest = contentChest;
            _gameStateService = gameStateService;
            _mapLoader = mapLoader;
            _mapRenderer = mapRenderer;
            _inputService = inputService;
        }

        public void Begin()
        {
            Ended = false;
            
            var activeMapId = _gameStateService.CurrentLevel;
            _activeMap = _mapLoader.LoadMap(activeMapId);
            _mapRenderer.SetMap(_activeMap);
            
            _inputService.OnKeyPressed(Keys.X, () =>
            {
                _inputService.Reset();
                Ended = true;
                RequestScreenChange?.Invoke(ScreenType.Game);
            });

            _activeMap.Begin();
        }

        public void Update()
        {
            foreach (var entity in _activeMap.Entities)
            {
                entity.Update();
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            spriteBatch.Draw(_contentChest.Get<Texture2D>("Images/pixel"), new Rectangle(0, 0, 1280, 720),
                new Color(9, 22, 48));

            spriteBatch.DrawString(_contentChest.Get<SpriteFont>("Fonts/MainFont"), _activeMap.Name,
                new Vector2(40, 40), Color.White);

            _mapRenderer.Render(spriteBatch);

            spriteBatch.End();
        }

        public bool Stop { get; set; }
    }
}