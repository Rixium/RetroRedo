using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Screen;

namespace RetroRedo.Entities
{
    public class WaitDoor : Entity
    {
        private int _waitTime;
        private int _currDoorImage;
        private float _doorTimer;
        private float _animSpeed = 0.1f;
        private int _maxDoorImage = 5;
        private int _minDoorImage = 1;

        public WaitDoor(in int tileX, in int tileY, int waitTime, bool blocking)
        {
            X = tileX;
            Y = tileY;
            _waitTime = waitTime;
            
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
            // Does nothing
        }

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

        public bool Closing { get; set; }

        public bool Opening { get; set; }

        public override void Render(SpriteBatch spriteBatch)
        {
            if (Closing || Opening ||!Blocking)
            {
                spriteBatch.Draw(ContentChest.Get<Texture2D>($"Images/wait_door{_currDoorImage}"),
                    new Vector2(Tile.RenderX, Tile.RenderY), Color.White);
            } else if (Blocking)
            {
                var waitDoor = ContentChest.Get<Texture2D>($"Images/wait_door_closed_{_waitTime}");
                spriteBatch.Draw(waitDoor, new Vector2(Tile.RenderX, Tile.RenderY), Color.White);
            }
            
            base.Render(spriteBatch);
        }

        public void Tick()
        {
            if (_waitTime <= 0)
            {
                return;
            }
            
            _waitTime--;

            if (_waitTime <= 0)
            {
                _waitTime = 0;
                ContentChest.Get<SoundEffect>("Sounds/pressure_plate").Play();
                Opening = true;
                Blocking = false;
            }
        }
    }
}