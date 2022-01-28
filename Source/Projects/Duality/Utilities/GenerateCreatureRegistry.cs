using Duality.Registries;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateCreatureRegistry
    {
        public static CreatureRegistry Perform(GameDriver driver)
        {
            Log.Message("GeneratingCreatureRegistry");
            return new CreatureRegistry();
        }
    }
}
