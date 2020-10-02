using System.Collections.Generic;
using System.Linq;

namespace RetroRedo.Screen
{
    public class ScreenProvider : IScreenProvider
    {
        public IReadOnlyCollection<IScreen> Screens { get; }

        public ScreenProvider(IReadOnlyCollection<IScreen> screens)
        {
            Screens = screens;
        }

        public IScreen GetScreen(ScreenType screenType) =>
            Screens.FirstOrDefault(x => x.ScreenType.Equals(screenType));
    }
}