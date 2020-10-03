using System;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;

namespace RetroRedo.Screen
{
    public class SplashScreen : IScreen
    {
        private const int MillisecondsToShowFor = 3000;
        private readonly IContentChest _contentChest;
        
        public ScreenType ScreenType => ScreenType.Splash;
        public bool Ended { get; set; }
        public Action<ScreenType> RequestScreenChange { get; set; }

        public SplashScreen(IContentChest contentChest) => _contentChest = contentChest;

        public void Begin()
        {
            var timer = new Timer
            {
                Interval = MillisecondsToShowFor,
            };

            timer.Elapsed += (x, y) =>
            {
                RequestScreenChange?.Invoke(ScreenType.MainMenu);
                Ended = true;
                timer.Stop();
            };

            timer.Start();
        }

        public void Update()
        {
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_contentChest.Get<Texture2D>("Images/splash"), new Rectangle(0, 0, 1280, 720),
                Color.White);
            spriteBatch.End();
        }
    }
}