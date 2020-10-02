namespace RetroRedo.Screen
{
    public class ScreenService : IScreenService
    {
        public IScreen CurrentScreen { get; private set; }
        public IScreen NextScreen { get; private set; }

        public void SetNextScreen(IScreen nextScreen)
        {
            NextScreen = nextScreen;
        }

        public void Update()
        {
            
        }
    }
}