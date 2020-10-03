using Microsoft.Xna.Framework;
using RetroRedo.Entities;
using RetroRedo.Window;

namespace RetroRedo
{
    public class Camera
    {
        private Vector2 _position;

        private int _minX;
        private readonly int _maxX;
        private int _minY;
        private readonly int _maxY;

        private int _zoom = 2;
        private IEntity _following;

        public Camera()
        {
            _position = Vector2.Zero;
            _maxX = 2000000;
            _maxY = 1000000;
        }

        public void Goto(Vector2 pos)
        {
            ToGo = pos;
        }

        public Vector2 ToGo { get; set; }

        public void Update(int maxX, int maxY)
        {
            maxX -= WindowSettings.WindowWidth / 2 / _zoom + 3;
            maxY -= WindowSettings.WindowHeight / 2 / _zoom + 3;
            _minX = (int) (WindowSettings.Center.X / _zoom + 3);
            _minY = (int) (WindowSettings.Center.Y / _zoom + 3);

            if (_following != null)
                Goto(new Vector2(_following.X + 16 * _zoom / 2.0f,
                    _following.Y + 16 * _zoom / 2.0f));

            var (x, y) = Vector2.Lerp(Position, ToGo, 0.03f);

            if (Vector2.Distance(Position, ToGo) < 10)
            {
                return;
            }

            _position.X = x;
            _position.Y = y;

            _position.X = MathHelper.Clamp(_position.X, _minX, maxX);
            _position.Y = MathHelper.Clamp(_position.Y, _minY, maxY);
        }

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public Matrix GetMatrix() =>
            Matrix.CreateTranslation(new Vector3(-_position.X, -_position.Y, 0)) *
            Matrix.CreateScale(_zoom, _zoom, 1) *
            Matrix.CreateTranslation(new Vector3(WindowSettings.Center.X, WindowSettings.Center.Y, 0));

        public bool Intersects(Rectangle bounds) =>
            Bounds.Intersects(bounds);

        public Rectangle Bounds => new Rectangle((int) (_position.X - WindowSettings.Center.X),
            (int) (_position.Y - WindowSettings.Center.Y), WindowSettings.WindowWidth, WindowSettings.WindowHeight);
    }
}