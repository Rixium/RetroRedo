using System.Collections.Generic;

namespace RetroRedo.Screen
{
    public class ScreenProvider : IScreenProvider
    {
        public IReadOnlyCollection<IScreen> Screens { get; }

        public ScreenProvider(IReadOnlyCollection<IScreen> screens)
        {
            Screens = screens;
        }
    }
}