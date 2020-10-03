using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Input;
using RetroRedo.Screen;
using RetroRedo.Window;

namespace RetroRedo
{
    public class Game1 : Game
    {
        public static IInputService Input;

        private readonly IScreenService _screenService;
        private readonly IGameTimeService _gameTimeService;
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            Input = new InputService();
            
            _screenService = new ScreenService();
            _gameTimeService = new GameTimeService();
            _graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _screenService.SetNextScreen(new SplashScreen());
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _graphics.PreferredBackBufferWidth = WindowSettings.WindowWidth;
            _graphics.PreferredBackBufferHeight = WindowSettings.WindowHeight;
            _graphics.ApplyChanges();

            ContentChest.SetContentManager(Content);
            ContentChest.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            _gameTimeService.Update(gameTime);
            Input.Update();
            
            _screenService.UpdateScreen(_gameTimeService.DeltaTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _screenService.RenderScreen(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}