using Microsoft.Xna.Framework;

namespace Duality.Components
{
    public class Transform2D
    {
        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;

        public static Transform2D Identity => new()
        {
            Position = Vector2.Zero,
            Rotation = 0.0f,
            Scale = Vector2.One
        };
    }
}
