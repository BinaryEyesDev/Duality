using System;

namespace Duality.Data
{
    public class ComponentEventArgs
        : EventArgs
    {
        public readonly Component Component;

        public ComponentEventArgs(Component component)
        {
            Component = component;
        }
    }
}
