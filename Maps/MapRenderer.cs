using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Data;
using RetroRedo.Window;

namespace RetroRedo.Maps
{
    public class MapRenderer : IMapRenderer
    {       
        private const int TileRenderSize = 32;
        private const int ActualTileSize = 16;

        private readonly IContentChest _contentChest;
        private readonly IWindowSettings _windowSettings;
        private TiledMap _tiledMap;

        public MapRenderer(IContentChest contentChest, IWindowSettings windowSettings)
        {
            _contentChest = contentChest;
            _windowSettings = windowSettings;
        }
        
        public void SetMap(TiledMap tiledMap) => _tiledMap = tiledMap;

        public void Render(SpriteBatch spriteBatch)
        {
            if (_tiledMap == null)
            {
                return;
            }
            
            var tiles = _tiledMap.Layers.First();
            var tileSet = _contentChest.Get<Texture2D>("Images/tiles_1");
            var screenCenter = _windowSettings.Center;
            var mapWidthInPixels = _tiledMap.Width * TileRenderSize;
            var mapHeightInPixels = _tiledMap.Height * TileRenderSize;
            var mapRenderPoint = screenCenter - new Vector2(mapWidthInPixels / 2.0f, mapHeightInPixels / 2.0f);

            for (var i = 0; i < tiles.Data.Length; i++)
            {
                var tileId = tiles.Data[i] - 1;

                var x = i % _tiledMap.Width;
                var y = i / _tiledMap.Height;

                var rectX = tileId % (tileSet.Width / 16);
                var rectY = tileId / (tileSet.Height / 16);

                var tileSourceRectangle = new Rectangle(rectX * ActualTileSize, rectY * ActualTileSize, ActualTileSize,
                    ActualTileSize);

                var tileDestinationRectangle = new Rectangle((int) (mapRenderPoint.X + x * TileRenderSize),
                    (int) (mapRenderPoint.Y + y * TileRenderSize), TileRenderSize, TileRenderSize);

                spriteBatch.Draw(tileSet, tileDestinationRectangle,
                    tileSourceRectangle, Color.White);
            }
        }
    }
}