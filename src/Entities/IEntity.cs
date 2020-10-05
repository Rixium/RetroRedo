using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Components;
using RetroRedo.Maps;

namespace RetroRedo.Entities
{
    public interface IEntity
    {
        public int X { get; }
        public int Y { get; }
        Map CurrentMap { get; set; }
        bool Blocking { get; }
        void Update();
        void AddComponent<T>(T component) where T : IComponent;
        T GetComponent<T>() where T : IComponent;
        void Begin();
        void Move(int xChange, int yChange);
        void Entered(IEntity other);
        void Left(IEntity other);
        void Render(SpriteBatch spriteBatch);
        void Wait();
        void AnyTimeUpdate();
    }
}