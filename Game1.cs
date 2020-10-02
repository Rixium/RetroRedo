using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;

namespace RetroRedo
{
    public class Game1 : Game
    {
        private readonly IContentChest _contentChest;
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1(IContentChest contentChest)
        {
            _contentChest = contentChest;
            _graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            
            _contentChest.SetContentManager(Content);
            _contentChest.Load();
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _spriteBatch.Begin();
            _spriteBatch.Draw(_contentChest.Get<Texture2D>("Images/splash"), new Rectangle(0, 0, 1280, 720), Color.White);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
