namespace RetroRedo.Screen
{
    public interface IScreenService
    {
        IScreen CurrentScreen { get; }
        IScreen NextScreen { get; }
        void SetNextScreen(IScreen nextScreen);
        void Update();
    }
}