using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public class ScreenService : IScreenService
    {
        private readonly IScreenProvider _screenProvider;

        private IScreen _currentScreen = new BlankScreen();

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

        public IScreen NextScreen { get; private set; }

        public ScreenService(IScreenProvider screenProvider)
        {
            _screenProvider = screenProvider;
        }

        public void SetNextScreen(ScreenType screenType)
        {
            NextScreen = _screenProvider.GetScreen(screenType);
            NextScreen.RequestScreenChange = OnScreenChangeRequest;
        }

        private void OnScreenChangeRequest(ScreenType screenType) => SetNextScreen(screenType);

        public void UpdateScreen()
        {
            if (CurrentScreen.Ended)
            {
                GoToNextScreen();
            }
            else
            {
                CurrentScreen.Update();
            }
        }

        private void GoToNextScreen()
        {
            CurrentScreen = NextScreen;
            CurrentScreen.Begin();
            NextScreen = null;
        }

        public void RenderScreen(SpriteBatch spriteBatch) => CurrentScreen.Render(spriteBatch);
    }
}