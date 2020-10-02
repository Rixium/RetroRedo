using System.Collections.Generic;

namespace RetroRedo.Screen
{
    public interface IScreenProvider
    {
        IReadOnlyCollection<IScreen> Screens { get; }
        IScreen GetScreen(ScreenType screenType);
    }
}