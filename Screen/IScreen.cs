using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public interface IScreen
    {
        ScreenType ScreenType { get; }     
        bool Ended { get; }
        void Update();
        void Render(SpriteBatch spriteBatch);
    }
}