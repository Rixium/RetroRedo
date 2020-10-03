using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroRedo.Components;
using RetroRedo.Content;
using RetroRedo.Entities;
using RetroRedo.Maps;
using RetroRedo.Services;
using RetroRedo.Window;

namespace RetroRedo.Screen
{

    public class GameScreen : IScreen
    {
        public static int CurrentMap = 1;
        public static int MapRefreshes = 0;
        
        private readonly MapLoader _mapLoader;
        private readonly MapRenderer _mapRenderer;
        private readonly MapEntityHistoryService _mapEntityHistoryService;

        private Map _activeMap;

        public ScreenType ScreenType => ScreenType.Game;
        public bool Ended { get; private set; }
        public Action<IScreen> RequestScreenChange { get; set; }

        public GameScreen()
        {
            _mapLoader = new MapLoader(new MapParser());
            _mapRenderer = new MapRenderer();
            _mapEntityHistoryService = new MapEntityHistoryService();
        }

        public void Begin()
        {
            _activeMap = _mapLoader.LoadMap(CurrentMap);
            _mapRenderer.SetMap(_activeMap);

            Game1.Input.OnKeyPressed(Keys.X, ResetMap);
            TurnService.PlayersTurn = true;

            foreach (var entity in _mapEntityHistoryService.GetHistoricalEntities())
            {
                entity.Begin();
            }
            
            _activeMap.Begin();
            
            Ended = false;
        }

        private void ResetMap()
        {
            foreach (var entity in _mapEntityHistoryService.GetHistoricalEntities())
            {
                var autoCommandComponent = entity.GetComponent<AutoCommandComponent>();
                autoCommandComponent.ForceFinish();
            }

            MapRefreshes++;
            SaveHistoricalEntities();
            
            Game1.Input.Reset();
            Ended = true;
            RequestScreenChange?.Invoke(new GameScreen());
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

        public void Update(float delta)
        {
            if (Ended) return;

            if (TurnService.PlayersTurn)
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

                EndTurn();
            }
        }

        private void EndTurn()
        {
            if (_activeMap.GetPlayerTile().IsWin)
            {
                EndLevel();
            }

            TurnService.PlayersTurn = true;
        }

        private void EndLevel()
        {
            ContentChest.Get<SoundEffect>("Sounds/Clap").Play();
            _mapEntityHistoryService.Reset();
            Game1.Input.Reset();
            TurnService.PlayersTurn = true;
            CurrentMap++;
            RequestScreenChange?.Invoke(new MapTransitionScreen());
            Ended = true;
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/pixel"), new Rectangle(0, 0, 1280, 720),
                new Color(9, 22, 48));

            spriteBatch.DrawString(ContentChest.Get<SpriteFont>("Fonts/MainFont"), _activeMap.Name,
                new Vector2(40, 40), Color.White);

            var font = ContentChest.Get<SpriteFont>("Fonts/MainFont");
            spriteBatch.DrawString(font, $"Redos: {MapRefreshes}",
                new Vector2(40,
                    WindowSettings.WindowHeight - font.MeasureString($"{MapRefreshes}").Y - 40),
                Color.White);

            _mapRenderer.Render(spriteBatch, _mapEntityHistoryService.GetHistoricalEntities());

            spriteBatch.End();
        }

        public void FadedOut()
        {

        }

        public bool Stop { get; set; }
    }
}