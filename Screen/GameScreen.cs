using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroRedo.Components;
using RetroRedo.Content;
using RetroRedo.Entities;
using RetroRedo.Input;
using RetroRedo.Maps;
using RetroRedo.Services;

namespace RetroRedo.Screen
{
    public class GameScreen : IScreen
    {
        private readonly IContentChest _contentChest;
        private readonly IGameStateService _gameStateService;
        private readonly IMapLoader _mapLoader;
        private readonly IMapRenderer _mapRenderer;
        private readonly IInputService _inputService;
        private readonly IMapEntityHistoryService _mapEntityHistoryService;
        private readonly ITurnService _turnService;

        private Map _activeMap;

        public ScreenType ScreenType => ScreenType.Game;
        public bool Ended { get; private set; }
        public Action<ScreenType> RequestScreenChange { get; set; }

        public GameScreen(IContentChest contentChest, IGameStateService gameStateService, IMapLoader mapLoader,
            IMapRenderer mapRenderer, IInputService inputService, IMapEntityHistoryService mapEntityHistoryService, ITurnService turnService)
        {
            _contentChest = contentChest;
            _gameStateService = gameStateService;
            _mapLoader = mapLoader;
            _mapRenderer = mapRenderer;
            _inputService = inputService;
            _mapEntityHistoryService = mapEntityHistoryService;
            _turnService = turnService;
        }

        public void Begin()
        {
            Ended = false;
            
            var activeMapId = _gameStateService.CurrentLevel;
            _activeMap = _mapLoader.LoadMap(activeMapId);
            _mapRenderer.SetMap(_activeMap);
            
            _inputService.OnKeyPressed(Keys.X, ResetMap);
            _turnService.PlayersTurn = true;

            foreach (var entity in _mapEntityHistoryService.GetHistoricalEntities())
            {
                entity.Begin();
            }
            
            _activeMap.Begin();
        }

        private void ResetMap()
        {
            SaveHistoricalEntities();
            _inputService.Reset();
            Ended = true;
            RequestScreenChange?.Invoke(ScreenType.Game);
        }

        private void SaveHistoricalEntities()
        {            
            var oldEntities = new List<IEntity>();

            foreach (var entity in _activeMap.Entities)
            {
                var entitiesCommandSet = entity.GetComponent<CommandSetComponent>();
                if (entitiesCommandSet == null) continue;
                oldEntities.Add(entity);
            }

            oldEntities.Add(_activeMap.Player);

            _mapEntityHistoryService.AddEntities(oldEntities);
        }

        public void Update()
        {
            if (_turnService.PlayersTurn)
            {
                _activeMap.Player.Update();
            }
            else
            {
                foreach (var entity in _activeMap.Entities)
                {
                    entity.Update();
                }

                foreach (var entity in _mapEntityHistoryService.GetHistoricalEntities())
                {
                    entity.Update();
                }

                _turnService.PlayersTurn = true;
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            spriteBatch.Draw(_contentChest.Get<Texture2D>("Images/pixel"), new Rectangle(0, 0, 1280, 720),
                new Color(9, 22, 48));

            spriteBatch.DrawString(_contentChest.Get<SpriteFont>("Fonts/MainFont"), _activeMap.Name,
                new Vector2(40, 40), Color.White);

            _mapRenderer.Render(spriteBatch, _mapEntityHistoryService.GetHistoricalEntities());

            spriteBatch.End();
        }

        public bool Stop { get; set; }
    }
}