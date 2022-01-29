using System.Collections.Generic;
using System.Linq;
using Duality.Agents;
using Duality.Data;

namespace Duality.Utilities
{
    public static class GenerateAdjacentDirection
    {
        public static List<DirectionInfo> Perform(GridIndex center)
        {
            var info = new DirectionInfo[8];
            info[0] = new DirectionInfo(center + new GridIndex(-1, +0), 270.0f);//left
            info[1] = new DirectionInfo(center + new GridIndex(-1, +1), 225.0f);//bottom_left
            info[2] = new DirectionInfo(center + new GridIndex(+0, +1), 180.0f);//bottom
            info[3] = new DirectionInfo(center + new GridIndex(+1, +1), 135.0f);//bottom_right
            info[4] = new DirectionInfo(center + new GridIndex(+1, +0), 90.0f);//right
            info[5] = new DirectionInfo(center + new GridIndex(+1, -1), 45.0f);//top_right
            info[6] = new DirectionInfo(center + new GridIndex(+0, -1), 0.0f);//top
            info[7] = new DirectionInfo(center + new GridIndex(-1, -1), 325.0f);//bottom_left

            return info.ToList();
        }
    }
}
