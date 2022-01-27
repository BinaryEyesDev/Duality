using System;
using System.Collections.Generic;
using Duality.Data;

namespace Duality
{
    public class GameEntity
    {
        public int Id;
        public string Name;
        public event EventHandler<ComponentEventArgs> OnComponentAdded;
        public event EventHandler<ComponentEventArgs> OnComponentRemoved;
        public int ComponentCount => _components.Count;

        public GameEntity(int id, string name = "Entity")
        {
            Id = id;
            Name = name;
        }

        public void AddComponent<T>() where T : Component, new()
        {
            var component = new T();
            _components.Add(component);
            OnComponentAdded?.Invoke(this, new ComponentEventArgs(component));
        }

        public void RemoveComponent(Component component)
        {
            if (_components.Remove(component))
                OnComponentRemoved?.Invoke(this, new ComponentEventArgs(component));
        }

        private readonly List<Component> _components = new();
    }
}
