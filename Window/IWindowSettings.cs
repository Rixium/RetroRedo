using Microsoft.Xna.Framework;

namespace RetroRedo.Window
{
    public interface IWindowSettings
    {
        int WindowWidth { get; set; }
        int WindowHeight { get; set; }
        Vector2 Center { get; }
    }
}