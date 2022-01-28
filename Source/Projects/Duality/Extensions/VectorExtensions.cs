using MGVector2 = Microsoft.Xna.Framework.Vector2;
using MGVector3 = Microsoft.Xna.Framework.Vector3;
using NuVector2 = System.Numerics.Vector2;
using NuVector3 = System.Numerics.Vector3;


namespace Duality.Extensions
{
    public static class VectorExtensions
    {
        public static NuVector2 ToNuVector2(this MGVector2 vector)
        {
            return new NuVector2(vector.X, vector.Y);
        }

        public static MGVector3 ToVector3(this MGVector2 vector, float z = 0.0f)
        {
            return new MGVector3(vector.X, vector.Y, z);
        }
    }
}
