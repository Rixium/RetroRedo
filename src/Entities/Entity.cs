﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Components;
using RetroRedo.Maps;

namespace RetroRedo.Entities
{
    public abstract class Entity : IEntity
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        private IList<IComponent> Components { get; } = new List<IComponent>();
        public Map CurrentMap { get; set; }
        public bool Blocking { get; protected set; }
        protected Tile Tile => CurrentMap.TileAt(X, Y);

        public virtual void Update()
        {
            foreach (var component in Components)
            {
                component.Update();
            }
        }

        public void AddComponent<T>(T component) where T : IComponent
        {
            Components.Add(component);
            component.Entity = this;
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

        public void Move(int xChange, int yChange)
        {
            Tile.OnExit(this);
            
            X += xChange;
            Y += yChange;
            
            Tile.OnEnter(this);
        }

        public abstract void Entered(IEntity other);
        public abstract void Left(IEntity other);

        public virtual void Render(SpriteBatch spriteBatch)
        {
            
        }

        public void Wait()
        {
            foreach (var entity in CurrentMap.Entities)
            {
                if (entity.GetType() != typeof(WaitDoor)) 
                    continue;
                
                var waitDoor = (WaitDoor) entity;
                waitDoor.Tick();
            }
        }

        public virtual void AnyTimeUpdate()
        {
        }
    }
}