using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public class GameScreen : IScreen
    {
        public ScreenType ScreenType => ScreenType.Game;
        public bool Ended { get; }
        public void Update()
        {
            
        }

        public void Render(SpriteBatch spriteBatch)
        {
            
        }

    }
}