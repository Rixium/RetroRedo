namespace RetroRedo.Screen
{
    public interface IScreen
    {
        ScreenType ScreenType { get; }     
        bool Ended { get; }
        void Update();
        void Render();
    }
}