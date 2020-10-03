using System;
using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public class MainMenuScreen : IScreen
    {
        public ScreenType ScreenType => ScreenType.MainMenu;

        public bool Ended { get; private set; }
        public Action<ScreenType> RequestScreenChange { get; set; }

        public void Begin()
        {
        }

        public void Update()
        {
        }

        public void Render(SpriteBatch spriteBatch)
        {
        }
    }
}