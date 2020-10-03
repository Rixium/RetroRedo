using System;
using System.Linq;
using RetroRedo.Data;

namespace RetroRedo.Maps
{
    public class MapParser : IMapParser
    {
        public Map Parse(TiledMap tiledMap)
        {
            var mapName = tiledMap.Properties.First(x => x.Name.Equals("Name")).Value;
            var mapTiles = ParseTiles(tiledMap);
            var map = new Map
            {
                Id = tiledMap.MapId,
                Name = mapName,
                Tiles = mapTiles,
                MapWidth = tiledMap.Width,
                MapHeight = tiledMap.Height
            };
            
            return map;
        }

        private static Tile[,] ParseTiles(TiledMap tiledMap)
        {
            var tileLayer = tiledMap.Layers.First(x => x.Name.Equals("Tiles", StringComparison.OrdinalIgnoreCase));
            var tileLayerHeight = tileLayer.Height;
            var tileLayerWidth = tileLayer.Width;
            var tiles = new Tile[tileLayerWidth, tileLayerHeight];

            for (var i = 0; i < tileLayer.Data.Length; i++)
            {
                var tileId = tileLayer.Data[i] - 1;

                var x = i % tileLayerWidth;
                var y = i / tileLayerHeight;

                var tile = new Tile(tileId, x, y);
                tiles[x, y] = tile;
            }

            return tiles;
        }
    }
}