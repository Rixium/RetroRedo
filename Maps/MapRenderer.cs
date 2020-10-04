using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Entities;
using RetroRedo.Window;

namespace RetroRedo.Maps
{
    public class MapRenderer : IMapRenderer
    {
        private const int TileRenderSize = 16;
        private const int ActualTileSize = 16;
        
        Random random = new Random();
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
            var screenCenter = WindowSettings.Center;
            var mapWidthInPixels = _map.MapWidth * TileRenderSize;
            var mapHeightInPixels = _map.MapHeight * TileRenderSize;

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
                    
                    var rainbow = Rainbow(entitiesOnPlayer / 10.0f);

                    spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/player"),
                        new Vector2(
                            entity.X * TileRenderSize,
                            entity.Y * TileRenderSize - 3 * entitiesOnPlayer),
                        new Color(rainbow.R, rainbow.G, rainbow.B) * 0.3f);
                }
                else
                {
                    var entityTile = _map.Tiles[entity.X, entity.Y];
                    var entitiesHere = entityTile.ThisFrameHistoryCount;

                    var rainbow = Rainbow(entitiesHere / 10.0f);
                    
                    spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/player"),
                        new Vector2(
                            entity.X * TileRenderSize,
                            entity.Y * TileRenderSize - 3 * entitiesHere),
                        new Color(rainbow.R, rainbow.G, rainbow.B) * 0.3f);

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
        
        public static System.Drawing.Color Rainbow(float progress)
        {
            var div = (Math.Abs(progress % 1) * 6);
            var ascending = (int) ((div % 1) * 255);
            var descending = 255 - ascending;

            switch ((int) div)
            {
                case 0:
                    return System.Drawing.Color.FromArgb(255, 255, ascending, 0);
                case 1:
                    return System.Drawing.Color.FromArgb(255, descending, 255, 0);
                case 2:
                    return System.Drawing.Color.FromArgb(255, 0, 255, ascending);
                case 3:
                    return System.Drawing.Color.FromArgb(255, 0, descending, 255);
                case 4:
                    return System.Drawing.Color.FromArgb(255, ascending, 0, 255);
                default: // case 5:
                    return System.Drawing.Color.FromArgb(255, 255, 0, descending);
            }
        }
    }
}