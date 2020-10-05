using Microsoft.Xna.Framework;
using RetroRedo.Window;

namespace RetroRedo.Utils
{
    public class Camera
    {
        private const int Zoom = 2;
        private Vector2 _position;

        public Camera() => _position = Vector2.Zero;

        public Vector2 Position
        {
            set => _position = value;
        }

        public Matrix GetMatrix() =>
            Matrix.CreateTranslation(new Vector3(-_position.X, -_position.Y, 0)) *
            Matrix.CreateScale(Zoom, Zoom, 1) *
            Matrix.CreateTranslation(new Vector3(WindowSettings.Center.X, WindowSettings.Center.Y, 0));
    }
}