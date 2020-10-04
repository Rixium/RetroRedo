using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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

        public MainMenuScreen()
        {
            MediaPlayer.Play(ContentChest.Get<Song>("Music/menu"));
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;

            Game1.ActiveSong = "Music/menu";
        }
        
        public void Begin()
        {
            GameScreen.CurrentMap = 1;
            
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

            
            var font = ContentChest.Get<SpriteFont>("Fonts/MainFont");

            spriteBatch.DrawString(font, "W: Move Up",
                new Vector2(WindowSettings.WindowWidth - font.MeasureString("W: Move Up").X - 40,
                    WindowSettings.WindowHeight - font.MeasureString("W: Move Up").Y * 6 - 90),
                Color.White);

            spriteBatch.DrawString(font, "D: Move Down",
                new Vector2(WindowSettings.WindowWidth - font.MeasureString("S: Move Down").X - 40,
                    WindowSettings.WindowHeight - font.MeasureString("S: Move Down").Y * 5 - 80),
                Color.White);

            spriteBatch.DrawString(font, "A: Move Left",
                new Vector2(WindowSettings.WindowWidth - font.MeasureString("A: Move Left").X - 40,
                    WindowSettings.WindowHeight - font.MeasureString("A: Move Left").Y * 4 - 70),
                Color.White);

            spriteBatch.DrawString(font, "D: Move Right",
                new Vector2(WindowSettings.WindowWidth - font.MeasureString("D: Move Right").X - 40,
                    WindowSettings.WindowHeight - font.MeasureString("D: Move Right").Y * 3 - 60),
                Color.White);

            spriteBatch.DrawString(font, "X: Redo",
                new Vector2(WindowSettings.WindowWidth - font.MeasureString("X: Redo").X - 40,
                    WindowSettings.WindowHeight - font.MeasureString("X: Redo").Y * 2 - 50),
                Color.White);

            spriteBatch.DrawString(font, "Z: Hard Restart",
                new Vector2(WindowSettings.WindowWidth - font.MeasureString("Z: Hard Restart").X - 40,
                    WindowSettings.WindowHeight - font.MeasureString("Z: Hard Restart").Y - 40),
                Color.White);
            
            spriteBatch.End();
        }

        public void FadedOut()
        {
        }
    }
}