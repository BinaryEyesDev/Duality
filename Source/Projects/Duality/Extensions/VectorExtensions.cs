using Duality.Data;
using XnaVector2 = Microsoft.Xna.Framework.Vector2;
using XnaVector3 = Microsoft.Xna.Framework.Vector3;
using NuVector2 = System.Numerics.Vector2;
using NuVector3 = System.Numerics.Vector3;


namespace Duality.Extensions
{
    public static class VectorExtensions
    {
        public static GridIndex ToGridIndex(this XnaVector2 vector)
        {
            return new GridIndex((int) vector.X, (int) vector.Y);
        }

        public static NuVector2 ToNuVector2(this GridIndex index)
        {
            return new NuVector2(index.Column, index.Row);
        }

        public static XnaVector2 ToXnaVector2(this GridIndex index)
        {
            return new XnaVector2(index.Column, index.Row);
        }

        public static NuVector2 ToNuVector2(this XnaVector2 vector)
        {
            return new NuVector2(vector.X, vector.Y);
        }

        public static XnaVector3 ToXnaVector3(this XnaVector2 vector, float z = 0.0f)
        {
            return new XnaVector3(vector.X, vector.Y, z);
        }
    }
}
