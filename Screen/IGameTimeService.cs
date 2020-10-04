using Microsoft.Xna.Framework;

namespace RetroRedo.Screen
{
    public interface IGameTimeService
    {
        static float DeltaTime { get; }

        void Update(GameTime gameTime);
    }
}