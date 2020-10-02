namespace RetroRedo.Screen
{
    public class ScreenService : IScreenService
    {
        private readonly IScreenProvider _screenProvider;
        
        public IScreen CurrentScreen { get; private set; }
        public IScreen NextScreen { get; private set; }

        public ScreenService(IScreenProvider screenProvider)
        {
            _screenProvider = screenProvider;
        }

        public void SetNextScreen(IScreen nextScreen)
        {
            NextScreen = nextScreen;
        }

        public void Update()
        {
        }
    }
}