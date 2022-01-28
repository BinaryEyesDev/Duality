using System;
using Microsoft.Xna.Framework;

namespace Duality.Data
{
    public class TileEventArgs
        : EventArgs
    {
        public readonly string Type;
        public readonly GridIndex Index;
        public readonly Vector2 WorldPosition;
        public readonly int LayerId;

        public TileEventArgs(string type, GridIndex index, Vector2 worldPosition, int layerId)
        {
            Type = type;
            Index = index;
            WorldPosition = worldPosition;
            LayerId = layerId;
        }
    }
}
