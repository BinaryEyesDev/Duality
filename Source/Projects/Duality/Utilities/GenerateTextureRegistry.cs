using Duality.Registries;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateTextureRegistry
    {
        public static TextureRegistry Perform(GameDriver driver)
        {
            Log.Message("GeneratingTextureRegistry");
            return new TextureRegistry(driver);
        }
    }
}
