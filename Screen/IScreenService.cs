using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public interface IScreenService
    {
        void SetNextScreen(IScreen screenType);
        void UpdateScreen(float delta);
        void RenderScreen(SpriteBatch spriteBatch);
    }
}