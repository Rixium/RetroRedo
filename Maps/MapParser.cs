using System;
using System.Collections.Generic;
using System.Linq;
using RetroRedo.Components;
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

            foreach (var mapEntity in mapEntities.OtherEntities)
            {
                mapTiles[mapEntity.X, mapEntity.Y].TileEntities.Add(mapEntity);
            }

            var map = new Map
            {
                Id = tiledMap.MapId,
                Name = mapName,
                Tiles = mapTiles,
                TileWidth = tiledMap.TileWidth,
                TileHeight = tiledMap.TileHeight,
                MapWidth = tiledMap.Width,
                MapHeight = tiledMap.Height,
                Player = mapEntities.Player,
                Entities = mapEntities.OtherEntities
            };

            return map;
        }

        private static (IEntity Player, IList<IEntity> OtherEntities) ParseEntities(TiledMap tiledMap)
        {
            var entityLayer = tiledMap.Layers.First(x => x.Name.Equals("Entities", StringComparison.OrdinalIgnoreCase));
            Player player = null;
            var entities = new List<IEntity>();

            foreach (var entityObject in entityLayer.Objects)
            {
                var tileX = (int) entityObject.X / tiledMap.TileWidth;
                var tileY = (int) entityObject.Y / tiledMap.TileHeight;
                
                if (entityObject.Type.Equals("PlayerStart", StringComparison.OrdinalIgnoreCase))
                {
                    player = new Player(tileX, tileY);
                    player.AddComponent(new PlayerMovementComponent());
                    player.AddComponent(new CommandSetComponent());
                }
                else
                {
                    if (entityObject.Type.Equals("DoorToggle", StringComparison.OrdinalIgnoreCase))
                    {
                        entities.Add(new DoorToggle(tileX, tileY, int.Parse(entityObject.Name)));
                    } else if (entityObject.Type.Equals("Door", StringComparison.OrdinalIgnoreCase))
                    {
                        var doorStatus = entityObject.Properties.FirstOrDefault(x => x.Name.Equals("Blocking"));
                        var blocking = bool.Parse(doorStatus?.Value ?? "false");
                        entities.Add(
                            new Door(tileX, tileY, int.Parse(entityObject.Name))
                            {
                                Blocking = blocking
                            });
                    } else if (entityObject.Type.Equals("WaitDoor", StringComparison.OrdinalIgnoreCase))
                    {
                        var doorStatus = entityObject.Properties.FirstOrDefault(x => x.Name.Equals("Blocking"));
                        var blocking = bool.Parse(doorStatus?.Value ?? "false");
                        entities.Add(
                            new WaitDoor(tileX, tileY, int.Parse(entityObject.Name))
                            {
                                Blocking = blocking
                            });
                    }
                }
            }

            return (player, entities);
        }

        private static Tile[,] ParseTiles(TiledMap tiledMap)
        {
            var tileLayer = tiledMap.Layers.First(x => x.Name.Equals("tiles", StringComparison.OrdinalIgnoreCase));
            var collidableLayer =
                tiledMap.Layers.First(x => x.Name.Equals("collidable", StringComparison.OrdinalIgnoreCase));
            var winLayer =
                tiledMap.Layers.First(x => x.Name.Equals("win", StringComparison.OrdinalIgnoreCase));

            var tileLayerHeight = tileLayer.Height;
            var tileLayerWidth = tileLayer.Width;
            var tiles = new Tile[tileLayerWidth, tileLayerHeight];

            for (var i = 0; i < tileLayer.Data.Length; i++)
            {
                var tileId = tileLayer.Data[i] - 1;
                var winId = winLayer.Data[i];
                var collideId = collidableLayer.Data[i];

                var x = i % tileLayerWidth;
                var y = i / tileLayerWidth;

                var tile = new Tile(tileId, x, y);
                tiles[x, y] = tile;

                if (winId != 0)
                {
                    tiles[x, y].IsWin = true;
                }

                if (collideId != 0)
                {
                    tiles[x, y].Collidable = true;
                }
            }

            return tiles;
        }
    }
}