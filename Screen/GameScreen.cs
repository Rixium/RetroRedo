using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private Camera _camera = new Camera();

        private static int _currentMapHistoryState;
        private static MapEntityHistoryService _mapEntityHistoryService;
        
        public static int CurrentMap = 1;
        public static int MapRefreshes = 0;
        
        private readonly MapLoader _mapLoader;
        private readonly MapRenderer _mapRenderer;

        private Map _activeMap;

        public ScreenType ScreenType => ScreenType.Game;
        public bool Ended { get; private set; }
        public Action<IScreen> RequestScreenChange { get; set; }

        public GameScreen()
        {
            _mapLoader = new MapLoader(new MapParser());
            _mapRenderer = new MapRenderer();

            if (CurrentMap > _currentMapHistoryState)
            {
                _currentMapHistoryState = CurrentMap;
                _mapEntityHistoryService = new MapEntityHistoryService();
            }

            if (Game1.ActiveSong != "Music/background")
            {
                MediaPlayer.Play(ContentChest.Get<Song>("Music/background"));
                MediaPlayer.IsRepeating = true;
                Game1.ActiveSong = "Music/background";
            }

            _activeMap = _mapLoader.LoadMap(CurrentMap);
            _mapRenderer.SetMap(_activeMap);
            
            _camera.Position = new Vector2(_activeMap.MapWidth * _activeMap.TileWidth / 2.0f, _activeMap.MapHeight * _activeMap.TileHeight / 2.0f);

            Game1.Input.OnKeyPressed(Keys.X, ResetMap);
            TurnService.PlayersTurn = true;

            foreach (var entity in _mapEntityHistoryService.GetHistoricalEntities())
            {
                entity.CurrentMap = _activeMap;
                entity.Begin();
            }
            
            _activeMap.Begin();
            
            Ended = false;
        }

        public void Begin()
        {

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
            var oldEntities = new List<IEntity>
            {
                _activeMap.Player
            };
            
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
            if (Ended)
            {
                return;
            }
            
            Ended = true;
            ContentChest.Get<SoundEffect>("Sounds/Clap").Play();
            _mapEntityHistoryService.Reset();
            Game1.Input.Reset();
            TurnService.PlayersTurn = true;
            CurrentMap++;

            if (_mapLoader.LoadMap(CurrentMap) == null)
            {
                RequestScreenChange?.Invoke(new MainMenuScreen());
            }
            else
            {
                RequestScreenChange?.Invoke(new MapTransitionScreen());
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            // Background
            spriteBatch.Begin();
            spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/pixel"), new Rectangle(0, 0, 1280, 720),
                new Color(62, 59, 86));
            spriteBatch.End();
            
            // Content
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _camera.GetMatrix());
            _mapRenderer.Render(spriteBatch, _mapEntityHistoryService.GetHistoricalEntities());
            spriteBatch.End();
            
            // UI
            spriteBatch.Begin();
            var titlefont = ContentChest.Get<SpriteFont>("Fonts/TitleFont");
            spriteBatch.DrawString(titlefont, _activeMap.Name, 
                new Vector2(1, 0) * WindowSettings.Center + new Vector2(0, 70) - titlefont.MeasureString(_activeMap.Name) / 2.0f, Color.White);

            var font = ContentChest.Get<SpriteFont>("Fonts/MainFont");
            spriteBatch.DrawString(font, $"Redos: {MapRefreshes}",
                new Vector2(40,
                    WindowSettings.WindowHeight - font.MeasureString($"{MapRefreshes}").Y - 40),
                Color.White);
            spriteBatch.End();
        }

        public void FadedOut()
        {

        }

        public bool Stop { get; set; }
    }
}