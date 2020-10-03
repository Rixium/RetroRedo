using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;

namespace RetroRedo.Entities
{
    public class Door : Entity
    {
        public int DoorId { get; }

        public Door(int tileX, int tileY, int doorId)
        {
            DoorId = doorId;
            X = tileX;
            Y = tileY;
        }

        public override void Entered(IEntity other)
        {
            // Does nothing.
        }

        public void Toggle() => Blocking = !Blocking;

        public void Close() => Blocking = true;

        public override void Render(SpriteBatch spriteBatch)
        {
            if (Blocking)
            {
                spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/closed_block"), new Vector2(Tile.RenderX, Tile.RenderY), Color.White);
            }
            
            base.Render(spriteBatch);
        }
    }
}