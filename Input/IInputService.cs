﻿using System;
using Microsoft.Xna.Framework.Input;

namespace RetroRedo.Input
{
    public interface IInputService
    {
        void Update();
        void OnKeyPressed(Keys keys, Action action);
        void Reset();
    }
}