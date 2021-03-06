using System.Linq;
using Duality.Components;
using Duality.Data;
using Duality.Editing;
using Duality.Extensions;
using Duality.Utilities;
using Microsoft.Xna.Framework;

namespace Duality.Agents
{
    public class Axolotl
        : IAgent
    {
        public AgentState State { get; set; }
        public Sprite Sprite { get; set; }
        public Transform2D Transform => Sprite.Transform;
        public DirectionInfo? TargetDirection;
        public float SwimSpeed;

        public Axolotl()
        {
            SwimSpeed = GetRandom.Float(4.0f, 8.0f);
        }

        public void SolveWorldActions(GameDriver driver)
        {
            TargetDirection ??= LookForTarget(Sprite.Transform);
            if (TargetDirection == null) return;

            var info = TargetDirection.Value;
            var targetPosition = CalculateGridFromWorld.GetWorldPosition(info.Index);
            var direction = Vector2.Normalize(targetPosition - Transform.Position);
            Transform.Rotation = info.Angle;
            Transform.Position += direction*SwimSpeed*FrameTime.ScaledElapsed;

            var distance = Vector2.Distance(Transform.Position, targetPosition);
            if (distance > 1.0f) return;
            TargetDirection = null;
        }

        public void Kill()
        {
            
        }

        public static DirectionInfo? LookForTarget(Transform2D transform)
        {
            DirectionInfo? direction = null;
            var waterLayerIndex = GameViewManager.CalculateLayerIndex("Water");
            var cellPosition = CalculateGridFromWorld.GetGridWorldPosition(transform.Position);
            var cell = CalculateGridFromWorld.GetGridIndex(cellPosition);
            var adjacentIndices = GenerateAdjacentDirectionInfos.Perform(cell);
            while (adjacentIndices.Count > 0 && direction == null)
            {
                var potential = adjacentIndices.PopRandom();
                if (!potential.Index.IsValid) continue;

                var potentialNode = GameDriver.Instance.World[potential.Index];
                var waterLayer = potentialNode.Layers[waterLayerIndex];
                if (waterLayer is not {Id: {SubGroup: "Water"}}) continue;
                if (potentialNode.Layers.Any(entry => entry is {Id: {SubGroup: "Grass"}})) continue;
                if (!CheckHasGrassEdgeNear(potential)) continue;

                direction = potential;
            }

            return direction;
        }

        private static bool CheckHasGrassEdgeNear(DirectionInfo potential)
        {
            var validCount = 0;
            var adjacentInfos = GenerateAdjacentDirectionInfos.Perform(potential.Index);
            foreach (var info in adjacentInfos)
            {
                var node = GameDriver.Instance.World[info.Index];
                var grass = node.Layers.FirstOrDefault(entry => entry is {Id: {SubGroup: "Grass"}});
                if (grass == null) continue;
                if (grass.Image.Name.Contains("5")) continue;
                validCount += 1;
            }

            return validCount > 0;
        }
    }
}
