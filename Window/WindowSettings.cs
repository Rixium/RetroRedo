using Microsoft.Xna.Framework;

namespace RetroRedo.Window
{
    public class WindowSettings : IWindowSettings
    {
        public int WindowWidth { get; set; } = 1280;
        public int WindowHeight { get; set; } = 720;
        public Vector2 Center => new Vector2(WindowWidth, WindowHeight) / 2;
    }
}