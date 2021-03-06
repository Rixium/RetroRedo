﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;

namespace RetroRedo.Entities
{
    internal class PressurePlate : Entity
    {
        public int DoorId { get; }
        private readonly HashSet<IEntity> _onEntities = new HashSet<IEntity>();

        public PressurePlate(int tileX, int tileY, int doorId)
        {
            X = tileX;
            Y = tileY;
            DoorId = doorId;
        }

        public override void Entered(IEntity other)
        {
            var added = _onEntities.Add(other);

            if (!added || _onEntities.Count != 1)
                return;
            
            var doors =
                CurrentMap?.Entities?.Where(x => x.GetType() == typeof(Door) && ((Door) x).DoorId == DoorId);

            if (doors == null) return;

            foreach (var entity in doors)
            {
                var door = (Door) entity;

                door.Requires--;

                if (door.Requires > 0) continue;
                    
                door.Requires = 0;
                        
                if (door.ClosedAtStart)
                {
                    door.Open();
                }
                else
                {
                    door.Close();
                }
            }

            ContentChest.Get<SoundEffect>("Sounds/pressure_on").Play();
        }

        public override void Left(IEntity other)
        {
            var removed = _onEntities.Remove(other);

            if (_onEntities.Count > 0)
            {
                return;
            }

            if (!removed) 
                return;
            
            var doors =
                CurrentMap?.Entities?.Where(x => x.GetType() == typeof(Door) && ((Door) x).DoorId == DoorId);

            if (doors == null) return;

            foreach (var entity in doors)
            {
                var door = (Door) entity;

                door.Requires++;

                if (door.Requires <= 0) 
                    continue;
                    
                if (door.ClosedAtStart)
                {
                    door.Close();
                }
                else
                {
                    door.Open();
                }
            }

            ContentChest.Get<SoundEffect>("Sounds/pressure_off").Play();
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            var image = _onEntities.Count > 0
                ? ContentChest.Get<Texture2D>("Images/constant_pressure_switch2")
                : ContentChest.Get<Texture2D>("Images/constant_pressure_switch1");
            
            spriteBatch.Draw(image, new Vector2(Tile.RenderX, Tile.RenderY), Color.White);
            
            base.Render(spriteBatch);
        }
    }
}