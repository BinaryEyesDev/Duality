using System.Collections.Generic;
using MonoGame.ImGui.Standard;

namespace Duality.Editing.Utilities
{
    public static class LoadTextureIcons
    {
        public static Dictionary<string, List<TexturePointerMapping>> Perform(GameDriver driver, ImGUIRenderer renderer)
        {
            var map = new Dictionary<string, List<TexturePointerMapping>>();
            foreach (var (typeName, textureList) in driver.TextureRegistry.Map)
            {
                if (!map.ContainsKey(typeName))
                    map.Add(typeName, new List<TexturePointerMapping>());

                foreach (var entry in textureList)
                {
                    var mapping = new TexturePointerMapping(typeName, entry.Name, renderer.BindTexture(entry));
                    map[typeName].Add(mapping);
                }
            }

            return map;
        }
    }
}
