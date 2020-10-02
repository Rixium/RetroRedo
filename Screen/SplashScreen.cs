using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;

namespace RetroRedo.Screen
{
    public class SplashScreen : IScreen
    {
        private readonly IContentChest _contentChest;
        public ScreenType ScreenType => ScreenType.Splash;
        public bool Ended { get; }

        public SplashScreen(IContentChest contentChest) => _contentChest = contentChest;

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