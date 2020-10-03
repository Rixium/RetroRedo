using System.Collections.Generic;
using System.Linq;
using RetroRedo.Components;
using RetroRedo.Maps;

namespace RetroRedo.Entities
{
    public abstract class Entity : IEntity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public IList<IComponent> Components { get; } = new List<IComponent>();
        public Map CurrentMap { get; set; }

        public virtual void Update()
        {
            foreach (var component in Components)
            {
                component.Update();
            }
        }

        public T AddComponent<T>(T component) where T : IComponent
        {
            Components.Add(component);
            component.Entity = this;
            return component;
        }

        public T GetComponent<T>() where T : IComponent =>
            (T) Components.FirstOrDefault(x => x.GetType() == typeof(T));

        public void Begin()
        {
            foreach (var component in Components)
            {
                component.Begin();
            }
        }

        public void RemoveComponent<T>() => Components.Remove(Components.First(x => x.GetType() == typeof(T)));
    }
}