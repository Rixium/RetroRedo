using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;

namespace RetroRedo.Entities
{
    public class DoorToggle : Entity
    {
        private readonly int _doorId;
        private bool _steppedOn;
        public DoorToggle(int tileX, int tileY, int doorId)
        {
            X = tileX;
            Y = tileY;
            _doorId = doorId;
        }

        public override void Entered(IEntity other)
        {
            if (_steppedOn) return;
            
            var doors = 
                CurrentMap?.Entities?.Where(x => x.GetType() == typeof(Door) && ((Door) x).DoorId == _doorId);

            if (doors == null) return;
            
            foreach (var entity in doors)
            {
                var door = (Door) entity;
                door.Toggle();
            }

            ContentChest.Get<SoundEffect>("Sounds/pressure_plate").Play();
            _steppedOn = true;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            var image = _steppedOn
                ? ContentChest.Get<Texture2D>("Images/pressure_switch2")
                : ContentChest.Get<Texture2D>("Images/pressure_switch1");
            
            spriteBatch.Draw(image, new Vector2(Tile.RenderX, Tile.RenderY), Color.White);
            
            base.Render(spriteBatch);
        }
    }
}