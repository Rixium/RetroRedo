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
        private readonly IWindowSettings _windowSettings;
        private readonly IContentChest _contentChest;
        private readonly IScreenService _screenService;
        private readonly IInputService _inputService;
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1(IWindowSettings windowSettings, IContentChest contentChest, IScreenService screenService, IInputService inputService)
        {
            _windowSettings = windowSettings;
            _contentChest = contentChest;
            _screenService = screenService;
            _inputService = inputService;
            _graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _screenService.SetNextScreen(ScreenType.Splash);
            
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _graphics.PreferredBackBufferWidth = _windowSettings.WindowWidth;
            _graphics.PreferredBackBufferHeight = _windowSettings.WindowHeight;
            _graphics.ApplyChanges();

            _contentChest.SetContentManager(Content);
            _contentChest.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            _inputService.Update();
            _screenService.UpdateScreen();
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