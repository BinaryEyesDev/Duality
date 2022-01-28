using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.ImGui.Standard;

namespace Duality.Editing.Utilities
{
    using TextureMappingMap = Dictionary<string, List<TexturePointerMapping>>;
    using TextureMap = Dictionary<string, List<Texture2D>>;

    public static class LoadTextureIcons
    {
        public static TextureMappingMap Perform(GameDriver driver, ImGUIRenderer renderer)
        {
            var map = new TextureMappingMap();
            GenerateIcons(map, renderer, driver.TextureRegistry.Tiles);
            GenerateIcons(map, renderer, driver.TextureRegistry.Objects);

            return map;
        }

        private static void GenerateIcons(TextureMappingMap map, ImGUIRenderer renderer, TextureMap textures)
        {
            foreach (var (typeName, textureList) in textures)
            {
                if (!map.ContainsKey(typeName))
                    map.Add(typeName, new List<TexturePointerMapping>());

                foreach (var entry in textureList)
                {
                    var mapping = new TexturePointerMapping(typeName, entry.Name, renderer.BindTexture(entry));
                    map[typeName].Add(mapping);
                }
            }
        }
    }
}
