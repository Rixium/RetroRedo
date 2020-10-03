using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroRedo.Content;
using RetroRedo.Input;
using RetroRedo.Window;

namespace RetroRedo.Screen
{
    public class MainMenuScreen : IScreen
    {
        private const string GameTitle = "RetroRedo";
        private const string StartGameText = "Press X to Redo";

        private readonly IWindowSettings _windowSettings;
        private readonly IGameTimeService _gameTimeService;
        private readonly IInputService _inputService;
        private readonly IContentChest _contentChest;
        public ScreenType ScreenType => ScreenType.MainMenu;

        public bool Ended { get; private set; }
        public Action<ScreenType> RequestScreenChange { get; set; }

        private FadeyText _startGameText;
        private Vector2 _gameTitleTextSize;
        private SpriteFont _titleFont;

        public MainMenuScreen(IWindowSettings windowSettings, IGameTimeService gameTimeService, IInputService inputService, IContentChest contentChest)
        {
            _windowSettings = windowSettings;
            _gameTimeService = gameTimeService;
            _inputService = inputService;
            _contentChest = contentChest;
        }

        public void Begin()
        {
            _titleFont = _contentChest.Get<SpriteFont>("Fonts/TitleFont");
            _gameTitleTextSize = _titleFont.MeasureString(GameTitle);

            var mainFont = _contentChest.Get<SpriteFont>("Fonts/MainFont");

            _inputService.OnKeyPressed(Keys.X, () =>
            {
                _inputService.Reset();
                RequestScreenChange?.Invoke(ScreenType.MapTransitionScreen);
                Ended = true;
            });
            
            _startGameText = new FadeyText(StartGameText, mainFont, _windowSettings.Center);
        }

        public void Update() => _startGameText.Update(_gameTimeService.DeltaTime);

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(_titleFont, GameTitle,
                _windowSettings.Center - (new Vector2(0, 1) * _windowSettings.Center / 2) - _gameTitleTextSize / 2,
                Color.White);
            
            _startGameText.Render(spriteBatch);

            spriteBatch.End();
        }
    }
}