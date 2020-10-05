using System;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Maps;
using RetroRedo.Window;

namespace RetroRedo.Screen
{
    public class MapTransitionScreen : IScreen
    {

        private readonly MapLoader _mapLoader;
        
        private string _mapName;
        private Vector2 _mapNameSize;
        private SpriteFont _mapNameFont;

        private double MillisecondsToShowFor { get; } = 1500;
        public bool Ended { get; private set; }
        public Action<IScreen> RequestScreenChange { get; set; }

        public MapTransitionScreen()
        {
            _mapLoader = new MapLoader(new MapParser());
        }
        
        public void Begin()
        {
            if (Ended) return;
            
            var activeLevel = GameScreen.CurrentMap;
            
            _mapNameFont = ContentChest.Get<SpriteFont>("Fonts/TitleFont");
            
            try
            {
                var activeMap = _mapLoader.LoadMap(activeLevel);
                _mapName = activeMap.Name;
                _mapNameSize = _mapNameFont.MeasureString(_mapName);
            }
            catch (Exception)
            {              
                _mapName = "";
                _mapNameSize = Vector2.Zero;
            }
            
            var timer = new Timer
            {
                Interval = MillisecondsToShowFor,
            };

            timer.Elapsed += (x, y) =>
            {
                timer.Stop();
                RequestScreenChange?.Invoke(new GameScreen());
                Ended = true;
            };

            timer.Start();
        }


        public void Update(float delta)
        {
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_mapNameFont, _mapName, WindowSettings.Center - _mapNameSize / 2, Color.White);
            spriteBatch.End();
        }

        public void FadedOut()
        {
            
        }
    }
}