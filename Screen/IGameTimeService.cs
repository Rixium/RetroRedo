using Microsoft.Xna.Framework;

namespace RetroRedo.Screen
{
    public interface IGameTimeService
    {
        float DeltaTime { get; }

        void Update(GameTime gameTime);
    }
}