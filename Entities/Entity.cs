using System.Collections.Generic;
using System.Linq;
using RetroRedo.Components;

namespace RetroRedo.Entities
{
    public abstract class Entity : IEntity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public IList<IComponent> Components { get; } = new List<IComponent>();

        public virtual void Update()
        {
            foreach (var component in Components)
            {
                component.Update();
            }
        }

        public void AddComponent(IComponent component)
        {
            Components.Add(component);
            component.Entity = this;
        }

        public T GetComponent<T>() where T : IComponent => 
            (T) Components.FirstOrDefault(x => x.GetType() == typeof(T));
    }
}