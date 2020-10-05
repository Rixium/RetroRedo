using System.Collections.Generic;
using RetroRedo.Entities;

namespace RetroRedo.Maps
{
    public class Tile
    {
        public int Id { get; }
        public int X { get; }
        public int Y { get; }
        public bool Collidable { get; set; }
        public bool IsWin { get; set; }

        public IList<IEntity> TileEntities { get; } = new List<IEntity>();
        public int RenderX => X * 16;
        public int RenderY => Y * 16;

        public Tile(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
        }

        public void OnEnter(IEntity other)
        {
            foreach (var entity in TileEntities)
            {
                entity.Entered(other);
            }
        }

        public int ThisFrameHistoryCount { get; set; }

        public void OnExit(Entity other)
        {
            foreach (var entity in TileEntities)
            {
                entity.Left(other);
            }
        }
    }
}