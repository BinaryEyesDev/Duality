using Duality.Components;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateWorldGrid
    {
        public static WorldGrid Perform()
        {
            Log.Message("GeneratingWorldGrid");
            return new WorldGrid();
        }
    }
}
