using Duality.Components;
using Duality.Utilities;

namespace Duality.Agents
{
    public class Axolotl
        : IAgent
    {
        public AgentState State { get; set; }
        public Sprite Sprite { get; set; }
        public float SwimSpeed;

        public Axolotl()
        {
            SwimSpeed = GetRandom.Float(4.0f, 8.0f);
        }

        public void SolveWorldActions(GameDriver driver)
        {
            
        }

        public void Kill()
        {
            
        }
    }
}
