using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Screen;

namespace RetroRedo.Entities
{
    public class Door : Entity
    {
        private const float AnimSpeed = 0.1f;
        private const float MaxDoorImage = 5;
        private const float MinDoorImage = 1;

        private float _doorTimer;
        private float _currDoorImage;

        private bool _open;
        private bool _closed;
        
        private bool Closing { get; set; }
        private bool Opening { get; set; }
        public bool Side { get; set; }
        public int Requires { get; set; }
        public bool ClosedAtStart { get; }
        
        public int DoorId { get; }

        public Door(int tileX, int tileY, int doorId, bool blocking)
        {
            DoorId = doorId;
            X = tileX;
            Y = tileY;

            Blocking = blocking;
            ClosedAtStart = blocking;
            if (blocking)
            {
                _closed = true;
                _currDoorImage = MinDoorImage;
            }
            else
            {
                _open = true;
                _currDoorImage = MaxDoorImage;
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
                ContentChest.Get<SoundEffect>("Sounds/pressure_plate").Play(0.5f, 0, 0);
                return;
            }

            if (!_closed && !Closing) return;
            
            Opening = true;
            Closing = false;
            _closed = false;
            ContentChest.Get<SoundEffect>("Sounds/pressure_plate").Play(0.5f, 0, 0);
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
                    if (_currDoorImage >= MaxDoorImage)
                    {
                        _currDoorImage = MaxDoorImage;
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
                if (_doorTimer > AnimSpeed)
                {
                    _currDoorImage--;
                    _doorTimer = 0;
                    if (_currDoorImage <= MinDoorImage)
                    {
                        _currDoorImage = MinDoorImage;
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


        public void Close()
        {            
            _doorTimer = 0;
            
            if (!_open && !Opening) 
                return;
            
            ContentChest.Get<SoundEffect>("Sounds/pressure_plate").Play();
            Closing = true;
            Blocking = true;
            Opening = false;
            _open = false;
        }

        public void Open()
        {
            if (!_closed && !Closing) 
                return;
            
            ContentChest.Get<SoundEffect>("Sounds/pressure_plate").Play();
            Opening = true;
            Closing = false;
            _closed = false;
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