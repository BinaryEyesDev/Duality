namespace Duality.Systems
{
    public class PerformAgentAI
        : GameSystem
    {
        public override void Perform(GameDriver driver)
        {
            for (var i = 0; i < driver.Agents.Count; i++)
                driver.Agents[i].SolveWorldActions(driver);
        }
    }
}
