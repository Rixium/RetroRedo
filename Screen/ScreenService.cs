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

        public void SetNextScreen(ScreenType screenType) => NextScreen = _screenProvider.GetScreen(screenType);

        public void UpdateScreen()
        {
            if (CurrentScreen.Ended)
                CurrentScreen = NextScreen;

            CurrentScreen.Update();
        }

        public void RenderScreen()
        {
            CurrentScreen.Render();
        }
    }
}