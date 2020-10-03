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

        private IList<IEntity> _mapEntities = new List<IEntity>();
    }
}