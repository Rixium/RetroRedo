using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Entities;

namespace RetroRedo.Maps
{
    public interface IMapRenderer
    {
        void SetMap(Map map);
        void Render(SpriteBatch spriteBatch, IList<IEntity> historical);
    }
}