using System;
using Duality.Data;

namespace Duality.Utilities
{
    public static class RunOnAllTiles
    {
        public static void Perform(Action<GridIndex, GameNode> action)
        {
            for (var row = 0; row < GlobalConfiguration.MapSize.Y; row++)
            {
                for (var col = 0; col < GlobalConfiguration.MapSize.X; col++)
                {
                    var index = new GridIndex(col, row);
                    var node = GameDriver.Instance.World.GetNode(index);
                    action(index, node);
                }
            }
        }
    }
}
