using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Maps
{
    public interface IMapRenderer
    {
        void SetMap(Map map);
        void Render(SpriteBatch spriteBatch);
    }
}