using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;

namespace RetroRedo.Entities
{
    public class Player : IEntity
    {
        private readonly IContentChest _contentChest;
        private readonly Vector2 _currentPosition;

        public Player(IContentChest contentChest, Vector2 startPosition)
        {
            _contentChest = contentChest;
            _currentPosition = startPosition;
        }

        public void Update()
        {
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_contentChest.Get<Texture2D>("Images/pixel"),
                new Rectangle((int) _currentPosition.X, (int) _currentPosition.Y, 32, 32), Color.White);
        }
    }
}