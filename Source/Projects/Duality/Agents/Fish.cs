using System;
using System.Collections.Generic;
using System.Linq;
using Duality.Components;
using Duality.Data;
using Duality.Extensions;
using Duality.Utilities;
using Orca.Logging;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Duality.Agents
{
    public class Fish
        : IAgent
    {
        private Vector2? _target;

        public float SwimSpeed;
        public Sprite Sprite;
        public Vector2 Forward = new(-1.0f, 0.0f);
        public Transform2D Transform => Sprite.Transform;

        public Fish()
        {
            SwimSpeed = GetRandom.Float(7.50f, 15.0f);
        }

        public void SolveWorldActions(GameDriver driver)
        {
            if (_target == null)
            {
                LookForTarget();
                return;
            }

            var direction = Vector2.Normalize(_target.Value - Transform.Position);
            var velocity = SwimSpeed*FrameTime.ScaledElapsed*direction;
            Transform.Position += velocity;

            var start = direction;
            var end = Forward;
            var angle = MathF.PI - (float)Math.Atan2(end.Y - start.Y, end.X - start.X);


            Log.Message($"Target={_target.Value}, Angle={angle.ToDegrees()}");

            var distance = Vector2.Distance(Transform.Position, _target.Value);
            if (distance > 5.0f) return;
            _target = null;
        }

        private void LookForTarget()
        {
            var cellPosition = CalculateGridFromWorld.GetGridWorldPosition(Transform.Position);
            var cell = CalculateGridFromWorld.GetGridIndex(cellPosition);
            var adjacentIndices = GenerateAdjacentIndices(cell);
            while (adjacentIndices.Count > 0 && _target == null)
            {
                var adjacentIndex = adjacentIndices.PopRandom();
                if (!adjacentIndex.IsValid)
                    continue;

                var adjacent = GameDriver.Instance.World[adjacentIndex];
                if (adjacent.Layers[0] == null || adjacent.Layers[0].Type != "Water")
                    continue;

                _target = CalculateGridFromWorld.GetWorldPosition(adjacentIndex);
            }
        }

        private List<GridIndex> GenerateAdjacentIndices(GridIndex center)
        {
            var indices = new GridIndex[4];
            indices[0] = center + new GridIndex(-1, +0);
            indices[1] = center + new GridIndex(+0, +1);
            indices[2] = center + new GridIndex(+1, +0);
            indices[3] = center + new GridIndex(+0, -1);

            return indices.ToList();
        }
    }
}
