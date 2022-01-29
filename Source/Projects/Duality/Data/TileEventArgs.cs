using System;
using Duality.Editing;
using Duality.Utilities;
using Microsoft.Xna.Framework;

namespace Duality.Data
{
    public class TileEventArgs
        : EventArgs
    {
        public readonly GameObjectId Id;
        public readonly GridIndex Index;
        public readonly Vector2 WorldPosition;
        public readonly int LayerId;

        public TileEventArgs(GameObjectId id, GridIndex index)
        {
            Id = id;
            Index = index;
            WorldPosition = CalculateGridFromWorld.GetWorldPosition(index);
            LayerId = GameViewManager.GetLayerId(id.SubGroup);
        }
    }
}
