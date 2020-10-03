using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Data;

namespace RetroRedo.Maps
{
    public interface IMapRenderer
    {
        void SetMap(TiledMap tiledMap);
        void Render(SpriteBatch spriteBatch);
    }
}