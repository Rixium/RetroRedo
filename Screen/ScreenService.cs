using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Window;

namespace RetroRedo.Screen
{
    public class ScreenService : IScreenService
    {
        private readonly IScreenProvider _screenProvider;
        private readonly IContentChest _contentChest;
        private readonly IWindowSettings _windowSettings;
        private readonly IGameTimeService _gameTimeService;

        private IScreen _currentScreen = new BlankScreen();

        private bool _transitioningOut;
        private bool _transitioningIn;
        private float _currentTransitionAlpha;

        public IScreen CurrentScreen
        {
            get => _currentScreen;
            private set
            {
                if (value != null)
                {
                    _currentScreen = value;
                }
            }
        }

        public float TransitionSpeed { get; set; } = 10f;
        public IScreen NextScreen { get; private set; }

        public ScreenService(IScreenProvider screenProvider, IContentChest contentChest, IWindowSettings windowSettings,
            IGameTimeService gameTimeService)
        {
            _screenProvider = screenProvider;
            _contentChest = contentChest;
            _windowSettings = windowSettings;
            _gameTimeService = gameTimeService;
        }

        public void SetNextScreen(ScreenType screenType)
        {
            NextScreen = _screenProvider.GetScreen(screenType);
            NextScreen.RequestScreenChange = OnScreenChangeRequest;
        }

        private void OnScreenChangeRequest(ScreenType screenType) => SetNextScreen(screenType);

        public void UpdateScreen()
        {
            if (_transitioningOut)
            {
                _currentTransitionAlpha += _gameTimeService.DeltaTime * TransitionSpeed;
                if (_currentTransitionAlpha >= 1)
                {
                    _transitioningOut = false;
                    GoToNextScreen();
                    _transitioningIn = true;
                }
            }
            else if (_transitioningIn)
            {
                _currentTransitionAlpha -= _gameTimeService.DeltaTime * TransitionSpeed;
                if (_currentTransitionAlpha <= 0)
                {
                    _transitioningIn = false;
                    _transitioningOut = false;
                }
            }
            else
            {
                if (CurrentScreen.Ended)
                {
                    _transitioningOut = true;
                }
                else
                {
                    CurrentScreen.Update();
                }
            }
        }


        private void GoToNextScreen()
        {
            CurrentScreen = NextScreen;
            CurrentScreen.Begin();
            NextScreen = null;
        }

        public void RenderScreen(SpriteBatch spriteBatch)
        {
            CurrentScreen.Render(spriteBatch);
            
            if (_transitioningIn || _transitioningOut)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(_contentChest.Get<Texture2D>("Images/pixel"),
                    new Rectangle(0, 0, _windowSettings.WindowWidth, _windowSettings.WindowHeight),
                    Color.Black * _currentTransitionAlpha);
                spriteBatch.End();
            }
        }
    }
}