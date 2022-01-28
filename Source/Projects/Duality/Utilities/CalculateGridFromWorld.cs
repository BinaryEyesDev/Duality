using System;
using Duality.Data;
using Microsoft.Xna.Framework;

namespace Duality.Utilities
{
    public static class CalculateGridFromWorld
    {
        public static Vector2 GetWorldPosition(GridIndex index)
        {
            var size = GlobalConfiguration.MapSize;
            return new Vector2(index.Column*size.X, index.Row*size.Y);
        }

        public static GridIndex GetGridIndex(Vector2 gridWorldPosition)
        {
            var indexPosition = gridWorldPosition/GlobalConfiguration.TileSize;
            return new GridIndex((int) indexPosition.X, (int) indexPosition.Y);
        }

        public static Vector2 GetGridWorldPosition(Vector2 worldPosition)
        {
            var mapSize = (GlobalConfiguration.MapSize-Vector2.One)*GlobalConfiguration.TileSize;
            var tileSize = GlobalConfiguration.TileSize;
            var result = new Vector2(
                worldPosition.X < 0.0f ? 0.0f : worldPosition.X > mapSize.X ? mapSize.X : worldPosition.X,
                worldPosition.Y < 0.0f ? 0.0f : worldPosition.Y > mapSize.Y ? mapSize.Y : worldPosition.Y);

            result += tileSize*0.5f;
            result.X = MathF.Truncate(result.X/tileSize.X);
            result.Y = MathF.Truncate(result.Y/tileSize.Y);

            result.X *= tileSize.X;
            result.Y *= tileSize.Y;

            return result;
        }
    }
}
