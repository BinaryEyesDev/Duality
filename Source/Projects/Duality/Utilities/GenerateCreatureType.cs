using Duality.Data;

namespace Duality.Utilities
{
    public static class GenerateCreatureType
    {
        public static string FromTextureName(GameElementTemplateInfo info)
        {
            if (info.TextureName.Contains("Fish"))
                return "Fish";

            if (info.TextureName.Contains("Turtle"))
                return "Turtle";

            if (info.TextureName.Contains("Axolotl"))
                return "Axolotl";

            if (info.TextureName.Contains("Boy"))
                return "Human";

            return "";
        }
    }
}
