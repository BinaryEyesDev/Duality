using Duality.Components;
using Duality.Data;
using Duality.Extensions;
using Duality.Spawners;
using Duality.Utilities;
using Orca.Logging;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Duality.Agents
{
    public class Fish
        : IAgent
    {
        private DirectionInfo? _targetInfo;

        public AgentState State { get; set; }
        public Sprite Sprite { get; set; }
        public float SwimSpeed;
        public Transform2D Transform => Sprite.Transform;

        public void Kill()
        {
            if (State == AgentState.Dead) return;

            Log.Message("KillingFish");
            State = AgentState.Dead;
            Sprite.IsDeleted = true;
            GameDriver.Instance.Agents.Remove(this);
            FishSpawner.Fishes.Remove(this);
        }

        public Fish()
        {
            SwimSpeed = GetRandom.Float(15.0f, 25.0f);
        }

        public void SolveWorldActions(GameDriver driver)
        {
            _targetInfo ??= LookForTarget(Transform);
            if (_targetInfo == null) return;

            var info = _targetInfo.Value;
            var targetPosition = CalculateGridFromWorld.GetWorldPosition(info.Index);
            var direction = Vector2.Normalize(targetPosition - Transform.Position);
            Transform.Rotation = info.Angle;
            Transform.Position += direction*SwimSpeed*FrameTime.ScaledElapsed;

            var distance = Vector2.Distance(Transform.Position, targetPosition);
            if (distance > 1.0f) return;
            _targetInfo = null;
        }

        public static DirectionInfo? LookForTarget(Transform2D transform)
        {
            DirectionInfo? direction = null;
            var cellPosition = CalculateGridFromWorld.GetGridWorldPosition(transform.Position);
            var cell = CalculateGridFromWorld.GetGridIndex(cellPosition);
            var adjacentIndices = GenerateAdjacentDirectionInfos.Perform(cell);
            while (adjacentIndices.Count > 0 && direction == null)
            {
                var adjacentInfo = adjacentIndices.PopRandom();
                if (!adjacentInfo.Index.IsValid)
                    continue;

                var adjacent = GameDriver.Instance.World[adjacentInfo.Index];
                if (adjacent.Layers[0] == null || adjacent.Layers[0].Type != "Water")
                    continue;

                direction = adjacentInfo;
            }

            return direction;
        }
    }
}
