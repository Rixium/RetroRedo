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
        private const int TileRenderSize = 32;
        private const int ActualTileSize = 16;
        
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
            var mapRenderPoint = screenCenter - new Vector2(mapWidthInPixels / 2.0f, mapHeightInPixels / 2.0f);

            foreach (var tile in _map.Tiles)
            {
                var rectX = tile.Id % (tileSet.Width / 16);
                var rectY = tile.Id / (tileSet.Height / 16);

                var tileSourceRectangle = new Rectangle(rectX * ActualTileSize, rectY * ActualTileSize, ActualTileSize,
                    ActualTileSize);
                var tileDestinationRectangle = new Rectangle((int) (mapRenderPoint.X + tile.X * TileRenderSize),
                    (int) (mapRenderPoint.Y + tile.Y * TileRenderSize), TileRenderSize, TileRenderSize);

                spriteBatch.Draw(tileSet, tileDestinationRectangle, tileSourceRectangle, Color.White);
            }

            foreach (var entity in _map.Entities)
            {
                spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/pixel"),
                    new Rectangle(
                        (int) (mapRenderPoint.X + entity.X * TileRenderSize),
                        (int) (mapRenderPoint.Y + entity.Y * TileRenderSize),
                        TileRenderSize,
                        TileRenderSize),
                    Color.White);
            }
            
            foreach (var entity in historical)
            {
                spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/pixel"),
                    new Rectangle(
                        (int) (mapRenderPoint.X + entity.X * TileRenderSize),
                        (int) (mapRenderPoint.Y + entity.Y * TileRenderSize),
                        TileRenderSize,
                        TileRenderSize),
                    Color.White);
            }
            
            spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/pixel"),
                new Rectangle(
                    (int) (mapRenderPoint.X + _map.Player.X * TileRenderSize),
                    (int) (mapRenderPoint.Y + _map.Player.Y * TileRenderSize),
                    TileRenderSize,
                    TileRenderSize),
                Color.White);
        }
    }
}