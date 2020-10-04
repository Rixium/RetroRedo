using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Maps;
using RetroRedo.Screen;

namespace RetroRedo.Entities
{
    public class Player : Entity
    {
        
        private float _colorTimer;
        private Color _currentColor;
        private float _current;

        public Player(int x, int y)
        {
            X = x;
            Y = y;
            
            var rainbow = MapRenderer.Rainbow(_current);
            _currentColor = new Color(rainbow.R, rainbow.G, rainbow.B);
        }

        public override void AnyTimeUpdate()
        {
            _colorTimer += GameTimeService.DeltaTime;

            if (_colorTimer >= 0.03f)
            {
                _current += 0.01f;
                if (_current > 1)
                {
                    _current = 0;
                }
                
                var rainbow = MapRenderer.Rainbow(_current);
                _currentColor = new Color(rainbow.R, rainbow.G, rainbow.B);
                _colorTimer = 0;
            }

            base.AnyTimeUpdate();
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
                new Vector2(Tile.RenderX, Tile.RenderY),
                _currentColor);
        }
    }
}