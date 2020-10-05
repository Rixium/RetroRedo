using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Screen;

namespace RetroRedo.Entities
{
    public class WaitDoor : Entity
    {
        private const float AnimSpeed = 0.1f;
        private const int MaxDoorImage = 5;
        private const int MinDoorImage = 1;
        private int _waitTime;
        private int _currDoorImage;
        private float _doorTimer;

        public WaitDoor(in int tileX, in int tileY, int waitTime, bool blocking)
        {
            X = tileX;
            Y = tileY;
            _waitTime = waitTime;
            
            Blocking = blocking;
            
            _currDoorImage = blocking ? MinDoorImage : MaxDoorImage;
        }

        public override void Entered(IEntity other)
        {
            // Does nothing
        }

        public override void Left(IEntity other)
        {
            
        }

        public override void AnyTimeUpdate()
        {
            if (Opening)
            {
                _doorTimer += GameTimeService.DeltaTime;
                if (_doorTimer > AnimSpeed)
                {
                    _currDoorImage++;
                    _doorTimer = 0;
                    if (_currDoorImage > MaxDoorImage)
                    {
                        _currDoorImage = MaxDoorImage;
                        Opening = false;
                        Blocking = false;
                    }
                }
            } else if (Closing)
            {
                _doorTimer += GameTimeService.DeltaTime;
                if (_doorTimer > AnimSpeed)
                {
                    _currDoorImage--;
                    if (_currDoorImage < MinDoorImage)
                    {
                        _currDoorImage = MinDoorImage;
                        Closing = false;
                        Blocking = true;
                    }
                    _doorTimer = 0;
                }
            }
            base.AnyTimeUpdate();
        }

        private bool Closing { get; set; }

        private bool Opening { get; set; }

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

            if (_waitTime > 0) 
                return;
            
            _waitTime = 0;
            ContentChest.Get<SoundEffect>("Sounds/pressure_plate").Play();
            Opening = true;
        }
    }
}