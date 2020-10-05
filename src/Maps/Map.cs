using System.Collections.Generic;
using System.Linq;
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
            var tile = Tiles[x, y];

            if (tile.TileEntities.Any(entity => entity.Blocking))
            {
                return false;
            }

            return !tile.Collidable;
        }

        public Tile GetPlayerTile() => Tiles[Player.X, Player.Y];

        public Tile TileAt(in int x, in int y) => Tiles[x, y];
    }
}