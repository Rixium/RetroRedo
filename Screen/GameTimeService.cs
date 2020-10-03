using Microsoft.Xna.Framework;

namespace RetroRedo.Screen
{
    class GameTimeService : IGameTimeService
    {
        public float DeltaTime { get; private set; }

        public void Update(GameTime gameTime)
        {
            DeltaTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
        }
    }
}