﻿using Microsoft.Xna.Framework;

namespace RetroRedo.Screen
{
    public class GameTimeService : IGameTimeService
    {
        public static float DeltaTime { get; private set; }

        public void Update(GameTime gameTime)
        {
            DeltaTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
        }
    }
}