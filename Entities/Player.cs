using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;

namespace RetroRedo.Entities
{
    public class Player : Entity
    {
        public Player(int x, int y)
        {
            X = x;
            Y = y;

        }
        
        public override void Entered(IEntity other)
        {
            // Does nothing.
        }

        public override void Left(IEntity other)
        {
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/player"),
                new Vector2(Tile.RenderX, Tile.RenderY), new Color(182, 158, 121));
        }
    }
}