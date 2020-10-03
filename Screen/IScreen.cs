using System;
using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public interface IScreen
    {
        ScreenType ScreenType { get; }     
        bool Ended { get; }
        Action<IScreen> RequestScreenChange { get; set; }
        void Begin();
        void Update(float delta);
        void Render(SpriteBatch spriteBatch);
        void FadedOut();
    }
}