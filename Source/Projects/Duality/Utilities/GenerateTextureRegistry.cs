using Duality.Registries;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateTextureRegistry
    {
        public static TextureRegistry Perform()
        {
            Log.Message("GeneratingTextureRegistry");
            return new TextureRegistry();
        }
    }
}
