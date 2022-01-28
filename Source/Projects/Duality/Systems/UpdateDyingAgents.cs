using System.Collections.Generic;
using Duality.Components;
using Microsoft.Xna.Framework;

namespace Duality.Systems
{
    public class UpdateDyingAgents
        : GameSystem
    {
        public override void Perform(GameDriver driver)
        {
            for (var i = 0; i < driver.Agents.Count; i++)
            {
                var agent = driver.Agents[i];
                if (agent.State != AgentState.Dying) continue;
                if (_dyingAgents.ContainsKey(agent)) continue;
                _dyingAgents.Add(agent, new DyingAgent(agent));
            }

            var dead = new List<IAgent>();
            foreach (var dyingAgent in _dyingAgents.Values)
            {
                dyingAgent.Update();
                if (dyingAgent.IsDead)
                    dead.Add(dyingAgent.Source);
            }

            foreach (var agent in dead)
                _dyingAgents.Remove(agent);
        }

        private readonly Dictionary<IAgent, DyingAgent> _dyingAgents = new();

    }

    public class DyingAgent
    {
        public readonly IAgent Source;
        public bool IsDead;
        public float TimeLeft;
        public int BlinksLeft;
        public Color Tint;
        public const int BlinkCount = 7;
        public const float BlinkTime = 0.5f;

        public void Update()
        {
            Source.Sprite.Tint = Tint;
            TimeLeft -= FrameTime.ScaledElapsed;
            if (TimeLeft > 0.0f) 
                return;

            Tint = BlinksLeft%2 != 0 ? Color.White : Color.Black;
            Source.Sprite.Tint = Tint;

            BlinksLeft -= 1;
            if (BlinksLeft == 0)
            {
                IsDead = true;
                Source.Kill();
                return;
            }

            TimeLeft = BlinkTime;
        }

        public DyingAgent(IAgent source)
        {
            Source = source;
            TimeLeft = BlinkTime;
            BlinksLeft = BlinkCount;
            Tint = Color.Black;
            IsDead = false;
        }
    }
}
