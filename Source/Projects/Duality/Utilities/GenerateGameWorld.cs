using Duality.Data;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateGameWorld
    {
        public static GameWorld Perform(GameDriver driver, string name)
        {
            Log.Message($"GeneratingGameWorld: {name}");
            return new GameWorld
            {
                Name = name,
            };
        }
    }
}
