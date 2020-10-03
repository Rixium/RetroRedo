using System;
using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public interface IScreen
    {
        ScreenType ScreenType { get; }     
        bool Ended { get; }
        Action<ScreenType> RequestScreenChange { get; set; }
        void Begin();
        void Update();
        void Render(SpriteBatch spriteBatch);
    }
}