using System;
using Microsoft.Xna.Framework;

namespace Duality.Utilities
{
    public static class CalculateGridFromWorld
    {
        public static Vector2 Perform(Vector2 worldPosition, Vector2 tileSize)
        {
            var result = new Vector2(
                MathF.Max(worldPosition.X, 0.0f), 
                MathF.Max(worldPosition.Y, 0.0f));
            
            result += tileSize*0.5f;
            result.X = MathF.Truncate(result.X/tileSize.X);
            result.Y = MathF.Truncate(result.Y/tileSize.Y);

            result.X *= tileSize.X;
            result.Y *= tileSize.Y;

            return result;
        }
    }
}
