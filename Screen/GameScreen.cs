using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Data;
using RetroRedo.Maps;

namespace RetroRedo.Screen
{
    public class GameScreen : IScreen
    {
        private readonly IContentChest _contentChest;
        private readonly IGameStateService _gameStateService;
        private readonly IMapLoader _mapLoader;
        private readonly IMapRenderer _mapRenderer;

        private TiledMap _activeTiledMap;

        public ScreenType ScreenType => ScreenType.Game;
        public bool Ended { get; private set; }
        public Action<ScreenType> RequestScreenChange { get; set; }

        public GameScreen(IContentChest contentChest, IGameStateService gameStateService, IMapLoader mapLoader,
            IMapRenderer mapRenderer)
        {
            _contentChest = contentChest;
            _gameStateService = gameStateService;
            _mapLoader = mapLoader;
            _mapRenderer = mapRenderer;
        }

        public void Begin()
        {
            var activeMapId = _gameStateService.CurrentLevel;
            _activeTiledMap = _mapLoader.LoadMap(activeMapId);
            _mapRenderer.SetMap(_activeTiledMap);
        }

        public void Update()
        {
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            spriteBatch.Draw(_contentChest.Get<Texture2D>("Images/pixel"), new Rectangle(0, 0, 1280, 720),
                new Color(9, 22, 48));

            spriteBatch.DrawString(_contentChest.Get<SpriteFont>("Fonts/MainFont"),
                _activeTiledMap.Properties.First(x => x.Name.Equals("Name")).Value,
                new Vector2(40, 40), Color.White);

            _mapRenderer.Render(spriteBatch);

            spriteBatch.End();
        }

        public bool Stop { get; set; }
    }
}