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

        private bool _open;
        private bool _closed;
        
        public int DoorId { get; }

        public Door(int tileX, int tileY, int doorId, bool blocking)
        {
            DoorId = doorId;
            X = tileX;
            Y = tileY;

            Blocking = blocking;
            
            if (blocking)
            {
                _closed = true;
                _currDoorImage = _minDoorImage;
            }
            else
            {
                _open = true;
                _currDoorImage = _maxDoorImage;
            }
        }

        public override void Entered(IEntity other)
        {
            // Does nothing.
        }

        public override void Left(IEntity other)
        {
            
        }

        public void Toggle()
        {
            _doorTimer = 0;
            if (_open || Opening)
            {
                Closing = true;
                Blocking = true;
                Opening = false;
                _open = false;
                return;
            }
            
            if (_closed || Closing)
            {
                Opening = true;
                Closing = false;
                _closed = false;
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
                    if (_currDoorImage >= _maxDoorImage)
                    {
                        _currDoorImage = _maxDoorImage;
                        Opening = false;
                        Blocking = false;
                        Closing = false; 
                        _open = true;
                        _closed = false;
                    }
                }
            }
            
            if (Closing)
            {
                _doorTimer += GameTimeService.DeltaTime;
                if (_doorTimer > _animSpeed)
                {
                    _currDoorImage--;
                    _doorTimer = 0;
                    if (_currDoorImage <= _minDoorImage)
                    {
                        _currDoorImage = _minDoorImage;
                        Closing = false;
                        Opening = false;
                        Blocking = true;
                        _closed = true;
                        _open = false;
                    }
                }
            }
            base.AnyTimeUpdate();
        }

        public bool Opening { get; set; }
        public bool Side { get; set; }
        public int Requires { get; set; }
        
        public void Close()
        {            
            _doorTimer = 0;
            if (_open || Opening)
            {
                Closing = true;
                Blocking = true;
                Opening = false;
                _open = false;
            }
        }

        public void Open()
        {
            if (_closed || Closing)
            {
                Opening = true;
                Closing = false;
                _closed = false;
            }
        }
        
        public override void Render(SpriteBatch spriteBatch)
        {
            var orientationExtra = "";

            if (Side)
            {
                orientationExtra = "side";
            }
            
            if (Closing || Opening ||!Blocking)
            {
                spriteBatch.Draw(ContentChest.Get<Texture2D>($"Images/closed_block_door{orientationExtra}{_currDoorImage}"),
                    new Vector2(Tile.RenderX, Tile.RenderY), Color.White);
            } else if (Blocking)
            {
                spriteBatch.Draw(ContentChest.Get<Texture2D>($"Images/closed_block{orientationExtra}"), new Vector2(Tile.RenderX, Tile.RenderY), Color.White);
            }
            
            base.Render(spriteBatch);
        }

    }
}