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

        public T GetComponent<T>() where T : Component
        {
            var found = _components.TryGetValue(typeof(T), out var component);
            return found ? (T) component : null;
        }

        public GameEntity(int id, string name = "Entity")
        {
            Id = id;
            Name = name;
        }

        public void AddComponent<T>() where T : Component, new()
        {
            var component = new T();
            _components.Add(typeof(T), component);
            OnComponentAdded?.Invoke(this, new ComponentEventArgs(component));
        }

        public void RemoveComponent<T>(T component) where T : Component
        {
            if (_components.Remove(component.GetType()))
                OnComponentRemoved?.Invoke(this, new ComponentEventArgs(component));
        }

        private readonly Dictionary<Type, Component> _components = new();
    }
}
