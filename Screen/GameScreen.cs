using System;
using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public class GameScreen : IScreen
    {
        public ScreenType ScreenType => ScreenType.Game;

        public bool Ended { get; private set; }
        public Action<ScreenType> RequestScreenChange { get; set; }

        public void Begin()
        {
        }

        public void Update()
        {
        }

        public void Render(SpriteBatch spriteBatch)
        {
        }
    }
}