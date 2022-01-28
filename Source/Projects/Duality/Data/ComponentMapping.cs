using System;
using System.Collections.Generic;
using System.Linq;

namespace Duality.Data
{
    public class ComponentMapping
    {
        public readonly int EntityId;

        public T GetComponent<T>() where T : Component
        {
            var found = _components.TryGetValue(typeof(T), out var component);
            if (!found)
                throw new NullReferenceException($"failed to locate component in mapping: id={EntityId}, type={typeof(T).Name}");

            return (T) component;
        }

        public ComponentMapping(int entityId, IEnumerable<Component> list)
        {
            EntityId = entityId;
            _components = list.ToDictionary(component => component.GetType());
        }

        private readonly Dictionary<Type, Component> _components;
    }
}
