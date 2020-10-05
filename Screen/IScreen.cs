using System;
using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public interface IScreen
    {
        bool Ended { get; }
        Action<IScreen> RequestScreenChange { set; }
        void Begin();
        void Update(float delta);
        void Render(SpriteBatch spriteBatch);
        void FadedOut();
    }
}