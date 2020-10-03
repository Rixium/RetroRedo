using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public interface IScreenService
    {
        IScreen CurrentScreen { get; }
        IScreen NextScreen { get; }
        void SetNextScreen(IScreen screenType);
        void UpdateScreen(float delta);
        void RenderScreen(SpriteBatch spriteBatch);
    }
}