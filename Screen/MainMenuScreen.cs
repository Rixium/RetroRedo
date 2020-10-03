using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroRedo.Content;
using RetroRedo.Window;

namespace RetroRedo.Screen
{
    public class MainMenuScreen : IScreen
    {
        private const string GameTitle = "RetroRedo";
        private const string StartGameText = "Press X to Redo";

        public ScreenType ScreenType => ScreenType.MainMenu;

        public bool Ended { get; private set; }
        public Action<IScreen> RequestScreenChange { get; set; }

        private FadeyText _startGameText;
        private Vector2 _gameTitleTextSize;
        private SpriteFont _titleFont;

        public void Begin()
        {
            _titleFont = ContentChest.Get<SpriteFont>("Fonts/TitleFont");
            _gameTitleTextSize = _titleFont.MeasureString(GameTitle);

            var mainFont = ContentChest.Get<SpriteFont>("Fonts/MainFont");

            Game1.Input.OnKeyPressed(Keys.X, () =>
            {
                Game1.Input.Reset();
                RequestScreenChange?.Invoke(new MapTransitionScreen());
                Ended = true;
            });

            _startGameText = new FadeyText(StartGameText, mainFont, WindowSettings.Center);
        }

        public void Update(float delta) => _startGameText.Update(delta);

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(_titleFont, GameTitle,
                WindowSettings.Center - (new Vector2(0, 1) * WindowSettings.Center / 2) - _gameTitleTextSize / 2,
                Color.White);

            _startGameText.Render(spriteBatch);

            spriteBatch.End();
        }

        public void FadedOut()
        {
        }
    }
}