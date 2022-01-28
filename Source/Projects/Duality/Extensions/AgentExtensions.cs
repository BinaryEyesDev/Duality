using System.Collections.Generic;

namespace Duality.Extensions
{
    public static class AgentExtensions
    {
        public static IReadOnlyList<T> FindAgentOfType<T>(this List<IAgent> agents) where T : IAgent
        {
            var found = new List<T>();
            foreach (var entry in agents)
            {
                if (entry is T agent)
                    found.Add(agent);
            }

            return found;
        }
    }
}
