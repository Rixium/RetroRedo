using System;
using System.Collections.Generic;
using System.Linq;
using RetroRedo.Data;
using RetroRedo.Entities;

namespace RetroRedo.Maps
{
    public class MapParser : IMapParser
    {
        public Map Parse(TiledMap tiledMap)
        {
            var mapName = tiledMap.Properties.First(x => x.Name.Equals("Name")).Value;
            var mapTiles = ParseTiles(tiledMap);
            var mapEntities = ParseEntities(tiledMap);

            var map = new Map
            {
                Id = tiledMap.MapId,
                Name = mapName,
                Tiles = mapTiles,
                TileWidth = tiledMap.TileWidth,
                TileHeight = tiledMap.TileHeight,
                MapWidth = tiledMap.Width,
                MapHeight = tiledMap.Height,
                Entities = mapEntities
            };

            return map;
        }

        private static IList<IEntity> ParseEntities(TiledMap tiledMap)
        {
            var entityLayer = tiledMap.Layers.First(x => x.Name.Equals("Entities", StringComparison.OrdinalIgnoreCase));
            var playerStartPosition = entityLayer
                .Objects.First(x =>
                    x.Properties.First(y => y.Name.Equals("Name", StringComparison.OrdinalIgnoreCase)).Value
                        .Equals("playerStart", StringComparison.OrdinalIgnoreCase));

            var entities = new List<IEntity>
            {
                new Player((int) playerStartPosition.X / tiledMap.TileWidth - 1,
                    (int) playerStartPosition.Y / tiledMap.TileHeight - 1)
            };

            return entities;
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