using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public class BlankScreen : IScreen
    {
        public ScreenType ScreenType => ScreenType.None;
        public bool Ended => true;

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