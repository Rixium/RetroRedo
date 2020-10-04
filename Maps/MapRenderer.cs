using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Entities;

namespace RetroRedo.Maps
{
    public class MapRenderer : IMapRenderer
    {
        private const int TileRenderSize = 16;
        private const int ActualTileSize = 16;
        
        HashSet<Tile> _historyTiles = new HashSet<Tile>();

        private Map _map;

        public void SetMap(Map map) => _map = map;

        public void Render(SpriteBatch spriteBatch, IList<IEntity> historical)
        {
            if (_map == null)
            {
                return;
            }

            var tileSet = ContentChest.Get<Texture2D>("Images/tiles_1");

            foreach (var tile in _map.Tiles)
            {
                var rectX = tile.Id % (tileSet.Width / 16);
                var rectY = tile.Id / (tileSet.Height / 16);

                var tileSourceRectangle = new Rectangle(rectX * ActualTileSize, rectY * ActualTileSize, ActualTileSize,
                    ActualTileSize);
                var tileDestinationRectangle = new Rectangle(tile.X * TileRenderSize,
                    tile.Y * TileRenderSize, TileRenderSize, TileRenderSize);

                spriteBatch.Draw(tileSet, tileDestinationRectangle, tileSourceRectangle, Color.White);
            }

            foreach (var entity in _map.Entities)
            {
                entity.Render(spriteBatch);
            }

            _map.Player.Render(spriteBatch);
            
            var entitiesOnPlayer = 0;

            foreach (var entity in historical)
            {
                if (entity.X == _map.Player.X && entity.Y == _map.Player.Y)
                {
                    entitiesOnPlayer++;
                    
                    spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/player"),
                        new Vector2(
                            entity.X * TileRenderSize,
                            entity.Y * TileRenderSize - 3 * entitiesOnPlayer),
                        new Color(176, 182, 121) * (1.0f / entitiesOnPlayer));
                }
                else
                {
                    var entityTile = _map.Tiles[entity.X, entity.Y];
                    var entitiesHere = entityTile.ThisFrameHistoryCount;
                    
                    spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/player"),
                        new Vector2(
                            entity.X * TileRenderSize,
                            entity.Y * TileRenderSize - 3 * entitiesHere),
                        new Color(176, 182, 121) * (1.0f / (entitiesHere + 1)));

                    entityTile.ThisFrameHistoryCount++;

                    _historyTiles.Add(entityTile);
                }
            }

            foreach (var tile in _historyTiles)
            {
                tile.ThisFrameHistoryCount = 0;
            }
            
            _historyTiles.Clear();
        }
        
    }
}