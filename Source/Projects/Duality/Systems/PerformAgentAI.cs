namespace Duality.Systems
{
    public class PerformAgentAI
        : GameSystem
    {
        public override void Perform(GameDriver driver)
        {
            for (var i = 0; i < driver.Agents.Count; i++)
            {
                var agent = driver.Agents[i];
                if (agent.State != AgentState.Alive) continue;
                agent.SolveWorldActions(driver);
            }
        }
    }
}
