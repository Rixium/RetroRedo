using System;
using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public class BlankScreen : IScreen
    {
        public ScreenType ScreenType => ScreenType.None;

        public bool Ended => true;
        public Action<IScreen> RequestScreenChange { get; set; }

        public void Begin()
        {
            // Does nothing
        }

        public void Update(float delta)
        {
            // Does nothing
        }

        public void Render(SpriteBatch spriteBatch)
        {
            // Does nothing
        }

        public void FadedOut()
        {
            
        }
    }
}