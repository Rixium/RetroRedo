using System;

namespace RetroRedo.Screen
{
    public class SplashScreen : IScreen
    {
        public ScreenType ScreenType => ScreenType.Splash;
        public bool Ended { get; }
        public void Update()
        {
            Console.WriteLine("Hello, World!");
        }

        public void Render()
        {
            
        }
    }
}