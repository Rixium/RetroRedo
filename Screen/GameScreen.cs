using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Exceptions;
using RetroRedo.Maps;

namespace RetroRedo.Screen
{
    public class GameScreen : IScreen
    {
        private readonly IContentChest _contentChest;
        private readonly IMapLoader _mapLoader;
        public ScreenType ScreenType => ScreenType.Game;

        public bool Ended { get; private set; }
        public Action<ScreenType> RequestScreenChange { get; set; }

        public GameScreen(IContentChest contentChest, IMapLoader mapLoader)
        {
            _contentChest = contentChest;
            _mapLoader = mapLoader;

            try
            {
                var currentMap = _mapLoader.LoadMap(1);
            }
            catch (MapNotExistException mapNotExistException)
            {
                Console.WriteLine(mapNotExistException.Message);
            }
        }
        
        public void Begin()
        {
        }

        public void Update()
        {
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_contentChest.Get<Texture2D>("Images/pixel"), new Rectangle(0, 0, 1280, 720), new Color(9, 22, 48));
            spriteBatch.End();
        }
    }
}