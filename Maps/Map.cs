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
        public int TileHeight { get; set; }
        public int TileWidth { get; set; }

        public void Begin()
        {
            foreach (var entity in Entities)
            {
                entity.Begin();
            }
        }

        public void AddEntities(IList<IEntity> oldEntities)
        {
            foreach (var entity in oldEntities) Entities.Add(entity);
        }
    }
}