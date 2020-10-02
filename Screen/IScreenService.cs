namespace RetroRedo.Screen
{
    public interface IScreenService
    {
        IScreen CurrentScreen { get; }
        IScreen NextScreen { get; }
        void SetNextScreen(ScreenType screenType);
        void UpdateScreen();
        void RenderScreen();
    }
}