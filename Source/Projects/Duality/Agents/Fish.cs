using System.Collections.Generic;
using System.Linq;
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
        private Vector2? _target;
        private float _targetAngle;

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
            if (_target == null) LookForTarget();
            if (_target == null) return;

            var direction = Vector2.Normalize(_target.Value - Transform.Position);
            Transform.Rotation = _targetAngle;
            Transform.Position += direction*SwimSpeed*FrameTime.ScaledElapsed;

            var distance = Vector2.Distance(Transform.Position, _target.Value);
            if (distance > 1.0f) return;

            _target = null;
        }

        private void LookForTarget()
        {
            var cellPosition = CalculateGridFromWorld.GetGridWorldPosition(Transform.Position);
            var cell = CalculateGridFromWorld.GetGridIndex(cellPosition);
            var adjacentIndices = GenerateAdjacentIndices(cell);
            while (adjacentIndices.Count > 0 && _target == null)
            {
                var adjacentInfo = adjacentIndices.PopRandom();
                if (!adjacentInfo.Index.IsValid)
                    continue;

                var adjacent = GameDriver.Instance.World[adjacentInfo.Index];
                if (adjacent.Layers[0] == null || adjacent.Layers[0].Type != "Water")
                    continue;

                _target = CalculateGridFromWorld.GetWorldPosition(adjacentInfo.Index);
                _targetAngle = adjacentInfo.Angle;
            }
        }

        private List<DirectionInfo> GenerateAdjacentIndices(GridIndex center)
        {
            var info = new DirectionInfo[8];
            info[0] = new DirectionInfo(center + new GridIndex(-1, +0), 270.0f);//left
            info[1] = new DirectionInfo(center + new GridIndex(-1, +1), 225.0f);//bottom_left
            info[2] = new DirectionInfo(center + new GridIndex(+0, +1), 180.0f);//bottom
            info[3] = new DirectionInfo(center + new GridIndex(+1, +1), 135.0f);//bottom_right
            info[4] = new DirectionInfo(center + new GridIndex(+1, +0), 90.0f);//right
            info[5] = new DirectionInfo(center + new GridIndex(+1, -1), 45.0f);//top_right
            info[6] = new DirectionInfo(center + new GridIndex(+0, -1), 0.0f);//top
            info[7] = new DirectionInfo(center + new GridIndex(-1, -1), 325.0f);//bottom_left

            return info.ToList();
        }
    }

    public readonly struct DirectionInfo
    {
        public readonly GridIndex Index;
        public readonly float Angle;

        public DirectionInfo(GridIndex index, float angle)
        {
            Index = index;
            Angle = angle;
        }
    }
}
