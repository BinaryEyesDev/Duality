using Duality.Components;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateWorldGrid
    {
        public static WorldGrid Perform(GameDriver driver)
        {
            Log.Message("GeneratingWorldGrid");
            return new WorldGrid(driver);
        }
    }
}
