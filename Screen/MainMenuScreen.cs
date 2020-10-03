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
        private readonly IInputService _inputService;
        private readonly IContentChest _contentChest;
        public ScreenType ScreenType => ScreenType.MainMenu;

        public bool Ended { get; private set; }
        public Action<ScreenType> RequestScreenChange { get; set; }

        private Vector2 _gameTitleTextSize;
        private Vector2 _startGameTextSize;
        private SpriteFont _mainFont;
        private SpriteFont _titleFont;

        public MainMenuScreen(IWindowSettings windowSettings, IInputService inputService, IContentChest contentChest)
        {
            _windowSettings = windowSettings;
            _inputService = inputService;
            _contentChest = contentChest;
        }

        public void Begin()
        {
            _titleFont = _contentChest.Get<SpriteFont>("Fonts/TitleFont");
            _gameTitleTextSize = _titleFont.MeasureString(GameTitle);

            _mainFont = _contentChest.Get<SpriteFont>("Fonts/MainFont");
            _startGameTextSize = _mainFont.MeasureString(StartGameText);

            _inputService.OnKeyPressed(Keys.X, () =>
            {
                RequestScreenChange?.Invoke(ScreenType.Game);
                Ended = true;
            });
        }

        public void Update()
        {
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(_titleFont, GameTitle,
                _windowSettings.Center - (new Vector2(0, 1) * _windowSettings.Center / 2) - _gameTitleTextSize / 2,
                Color.White);

            spriteBatch.DrawString(_mainFont, StartGameText,
                _windowSettings.Center - _startGameTextSize / 2,
                Color.White);

            spriteBatch.End();
        }
    }
}