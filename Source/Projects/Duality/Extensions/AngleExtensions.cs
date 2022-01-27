using Microsoft.Xna.Framework;

namespace Duality.Extensions
{
    public static class AngleExtensions
    {
        public static float ToRadians(this float degrees) => MathHelper.ToRadians(degrees);
        public static float ToDegrees(this float radians) => MathHelper.ToDegrees(radians);
    }
}
