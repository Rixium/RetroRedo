using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Screen;

namespace RetroRedo.Entities
{
    public class Door : Entity
    {

        private float _doorTimer;
        private float _animSpeed = 0.1f;
        private float _currDoorImage = 1;
        private float _maxDoorImage = 5;
        private float _minDoorImage = 1;
        
        public int DoorId { get; }

        public Door(int tileX, int tileY, int doorId, bool blocking)
        {
            DoorId = doorId;
            X = tileX;
            Y = tileY;

            Blocking = blocking;
            
            if (blocking)
            {
                _currDoorImage = _minDoorImage;
            }
            else
            {
                _currDoorImage = _maxDoorImage;
            }
        }

        public override void Entered(IEntity other)
        {
            // Does nothing.
        }

        public void Toggle()
        {
            if (Blocking)
            {
                Blocking = false;
                Opening = true;
                Closing = false;
            }
            else
            {
                Blocking = true;
                Closing = true;
                Opening = false;
            }
        }

        public bool Closing { get; set; }

        public override void AnyTimeUpdate()
        {
            if (Opening)
            {
                _doorTimer += GameTimeService.DeltaTime;
                if (_doorTimer > _animSpeed)
                {
                    _currDoorImage++;
                    _doorTimer = 0;
                    if (_currDoorImage > _maxDoorImage)
                    {
                        _currDoorImage = _maxDoorImage;
                        Opening = false;
                    }
                }
            } else if (Closing)
            {
                _doorTimer += GameTimeService.DeltaTime;
                if (_doorTimer > _animSpeed)
                {
                    _currDoorImage--;
                    if (_currDoorImage < _minDoorImage)
                    {
                        _currDoorImage = _minDoorImage;
                        Closing = false;
                    }
                    _doorTimer = 0;
                }
            }
            base.AnyTimeUpdate();
        }

        public bool Opening { get; set; }

        public void Close() => Blocking = true;

        public override void Render(SpriteBatch spriteBatch)
        {
            if (Closing || Opening ||!Blocking)
            {
                spriteBatch.Draw(ContentChest.Get<Texture2D>($"Images/closed_block_door{_currDoorImage}"),
                    new Vector2(Tile.RenderX, Tile.RenderY), Color.White);
            } else if (Blocking)
            {
                spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/closed_block"), new Vector2(Tile.RenderX, Tile.RenderY), Color.White);
            }
            
            base.Render(spriteBatch);
        }
    }
}