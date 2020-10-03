using System.Collections.Generic;
using RetroRedo.Components;

namespace RetroRedo.Entities
{
    public interface IEntity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public IList<IComponent> Components { get; }
        void Update();
        void AddComponent(IComponent component);
        T GetComponent<T>() where T : IComponent;
        void Begin();
    }
}