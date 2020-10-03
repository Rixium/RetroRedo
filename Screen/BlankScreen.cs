using System;
using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public class BlankScreen : IScreen
    {
        public ScreenType ScreenType => ScreenType.None;

        public bool Ended => true;
        public Action<ScreenType> RequestScreenChange { get; set; }

        public void Begin()
        {
            // Does nothing
        }

        public void Update()
        {
            // Does nothing
        }

        public void Render(SpriteBatch spriteBatch)
        {
            // Does nothing
        }

    }
}