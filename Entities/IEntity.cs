using System.Collections.Generic;
using RetroRedo.Components;
using RetroRedo.Maps;

namespace RetroRedo.Entities
{
    public interface IEntity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public IList<IComponent> Components { get; }
        Map CurrentMap { get; set; }
        void Update();
        T AddComponent<T>(T component) where T : IComponent;
        T GetComponent<T>() where T : IComponent;
        void Begin();
        void RemoveComponent<T>();
    }
}