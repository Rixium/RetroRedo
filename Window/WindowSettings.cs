using Microsoft.Xna.Framework;

namespace RetroRedo.Window
{
    public static class WindowSettings
    {
        public static int WindowWidth { get; set; } = 1280;
        public static int WindowHeight { get; set; } = 720;
        public static Vector2 Center => new Vector2(WindowWidth, WindowHeight) / 2;
    }
}