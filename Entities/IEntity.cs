using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Entities
{
    public interface IEntity
    {
        void Update();
        void Render(SpriteBatch spriteBatch);
    }
}