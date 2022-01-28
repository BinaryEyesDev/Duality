using Duality.Components;
using Duality.Extensions;
using Duality.Utilities;
using Microsoft.Xna.Framework;

namespace Duality.Agents
{
    public class Fish
        : IAgent
    {
        public float SwimSpeed = 5.0f;
        public Sprite Sprite;
        public Vector2 Forward = new(-1.0f, 0.0f);
        public Transform2D Transform => Sprite.Transform;
        
        public void SolveWorldActions(GameDriver driver)
        {
            var rotation = Matrix.CreateRotationY(Transform.Rotation.ToRadians());
            var direction = Vector2.Transform(Forward, rotation);
            var velocity = direction*SwimSpeed*FrameTime.ScaledElapsed;
            Transform.Position += velocity;

            if (Transform.Position.X < 0.0f)
            {
                Transform.Rotation = 180.0f;
                return;
            }

            var gridIndex = CalculateGridFromWorld.GetGridWorldPosition(Transform.Position);

        }
    }
}
