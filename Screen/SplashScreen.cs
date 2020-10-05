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

        public bool Ended { get; private set; }
        public Action<IScreen> RequestScreenChange { get; set; }
        

        public void Begin()
        {
            var timer = new Timer
            {
                Interval = MillisecondsToShowFor
            };

            timer.Elapsed += (x, y) =>
            {
                RequestScreenChange?.Invoke(new MainMenuScreen());
                Ended = true;
                timer.Stop();
            };

            timer.Start();
        }

        public void Update(float delta)
        {
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/splash"), new Rectangle(0, 0, 1280, 720),
                Color.White);
            spriteBatch.End();
        }

        public void FadedOut()
        {
            
        }
    }
}