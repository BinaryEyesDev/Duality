using Duality.Components;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateFramerateDisplay
    {
        public static FramerateDisplay Perform(GameDriver driver)
        {
            Log.Message("GeneratingFramerateDisplay");
            return new FramerateDisplay(driver);
        }
    }
}
