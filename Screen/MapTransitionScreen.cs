using System;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Maps;
using RetroRedo.Window;

namespace RetroRedo.Screen
{
    public class MapTransitionScreen : IScreen
    {
        private readonly IGameStateService _gameStateService;
        private readonly IMapLoader _mapLoader;
        private readonly IContentChest _contentChest;
        private readonly IWindowSettings _windowSettings;

        private string _mapName;
        private Vector2 _mapNameSize;
        private SpriteFont _mapNameFont;

        private double MillisecondsToShowFor { get; } = 500;
        public ScreenType ScreenType => ScreenType.MapTransitionScreen;
        public bool Ended { get; private set; }
        public Action<ScreenType> RequestScreenChange { get; set; }

        public MapTransitionScreen(IGameStateService gameStateService, IMapLoader mapLoader, IContentChest contentChest,
            IWindowSettings windowSettings)
        {
            _gameStateService = gameStateService;
            _mapLoader = mapLoader;
            _contentChest = contentChest;
            _windowSettings = windowSettings;
        }

        public void Begin()
        {
            var activeLevel = _gameStateService.CurrentLevel;
            var activeMap = _mapLoader.LoadMap(activeLevel);
            _mapName = activeMap.Name;
            _mapNameFont = _contentChest.Get<SpriteFont>("Fonts/TitleFont");
            _mapNameSize = _mapNameFont.MeasureString(_mapName);

            var timer = new Timer
            {
                Interval = MillisecondsToShowFor,
            };

            timer.Elapsed += (x, y) =>
            {
                RequestScreenChange?.Invoke(ScreenType.Game);
                Ended = true;
                timer.Stop();
            };

            timer.Start();
        }


        public void Update()
        {
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_mapNameFont, _mapName, _windowSettings.Center - _mapNameSize / 2, Color.White);
            spriteBatch.End();
        }
    }
}