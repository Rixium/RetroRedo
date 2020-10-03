using System.Collections.Generic;
using RetroRedo.Entities;

namespace RetroRedo.Maps
{
    public class Map
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public Tile[,] Tiles { get; set; }
        public IList<IEntity> Entities { get; set; }
        public IEntity Player { get; set; }
        public int TileHeight { get; set; }
        public int TileWidth { get; set; }

        public void Begin()
        {
            foreach (var entity in Entities)
            {
                entity.CurrentMap = this;
                entity.Begin();
            }

            Player.CurrentMap = this;
            Player.Begin();
        }

        public bool TileIsOpen(int x, int y)
        {
            if (x < 0 || y < 0 || x >= MapWidth || y >= MapHeight) return false;
            return !Tiles[x, y].Collidable;
        }

        public Tile GetPlayerTile() => Tiles[Player.X, Player.Y];
    }
}