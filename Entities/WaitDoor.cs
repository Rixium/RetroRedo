using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;

namespace RetroRedo.Entities
{
    public class WaitDoor : Entity
    {
        private int _waitTime;

        public WaitDoor(in int tileX, in int tileY, int waitTime)
        {
            X = tileX;
            Y = tileY;
            _waitTime = waitTime;
        }

        public override void Entered(IEntity other)
        {
            // Does nothing
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            if (Blocking)
            {
                var waitDoor = ContentChest.Get<Texture2D>($"Images/wait_door_{_waitTime}");
                spriteBatch.Draw(waitDoor, new Vector2(Tile.RenderX, Tile.RenderY), Color.White);
            }
            
            base.Render(spriteBatch);
        }

        public void Tick()
        {
            _waitTime--;

            if (_waitTime <= 0)
            {
                ContentChest.Get<SoundEffect>("Sounds/pressure_plate").Play();
                Blocking = !Blocking;
            }
        }
    }
}