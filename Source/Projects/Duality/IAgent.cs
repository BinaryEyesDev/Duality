using Duality.Components;

namespace Duality
{
    public enum AgentState { Alive, Dying, Dead, }

    public interface IAgent
    {
        AgentState State { get; set; }
        Sprite Sprite { get; }
        void SolveWorldActions(GameDriver driver);
        void Kill();
    }
}
