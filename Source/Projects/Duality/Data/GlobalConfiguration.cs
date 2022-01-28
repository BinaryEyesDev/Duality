using Duality.Extensions;
using Microsoft.Xna.Framework;
using NuVector2 = System.Numerics.Vector2;


namespace Duality.Data
{
    public static class GlobalConfiguration
    {
        public static readonly Vector2 TileSize = new Vector2(64.0f, 64.0f);
        public static readonly Vector2 MapSize = new Vector2(20.0f, 20.0f);
        public static readonly Vector2 CameraStart = TileSize*(MapSize*0.5f);

        public static readonly NuVector2 GuiTileSize = TileSize.ToNuVector2();
        public static readonly NuVector2 GuiTileIconSize = GuiTileSize*0.5f;

    }
}
