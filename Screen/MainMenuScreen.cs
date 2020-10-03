using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Window;

namespace RetroRedo.Screen
{
    public class MainMenuScreen : IScreen
    {
        private readonly IWindowSettings _windowSettings;
        private readonly IContentChest _contentChest;
        public ScreenType ScreenType => ScreenType.MainMenu;

        public bool Ended { get; private set; }
        public Action<ScreenType> RequestScreenChange { get; set; }

        private const string StartGameText = "Press X to Redo";
        private Vector2 _startGameTextSize;
        private SpriteFont _mainFont;

        public MainMenuScreen(IWindowSettings windowSettings, IContentChest contentChest)
        {
            _windowSettings = windowSettings;
            _contentChest = contentChest;
        }

        public void Begin()
        {
            _mainFont = _contentChest.Get<SpriteFont>("Fonts/MainFont");
            _startGameTextSize = _mainFont.MeasureString(StartGameText);
        }

        public void Update()
        {
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_mainFont, StartGameText,
                (new Vector2(_windowSettings.WindowWidth, _windowSettings.WindowHeight) / 2) - (_startGameTextSize / 2),
                Color.White);
            spriteBatch.End();
        }
    }
}