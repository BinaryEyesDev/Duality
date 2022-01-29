using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.ImGui.Standard;

namespace Duality.Editing.Utilities
{
    using TextureMappingMap = Dictionary<string, List<GameElementTemplateInfo>>;
    using TextureMap = Dictionary<string, List<Texture2D>>;

    public static class LoadTextureIcons
    {
        public static TextureMappingMap Perform(GameDriver driver, ImGUIRenderer renderer)
        {
            var map = new TextureMappingMap();
            GenerateIcons(map, renderer, driver.TextureRegistry.Groupings["Tiles"]);
            GenerateIcons(map, renderer, driver.TextureRegistry.Groupings["Objects"]);

            return map;
        }

        private static void GenerateIcons(TextureMappingMap map, ImGUIRenderer renderer, TextureMap textures)
        {
            foreach (var (typeName, textureList) in textures)
            {
                if (!map.ContainsKey(typeName))
                    map.Add(typeName, new List<GameElementTemplateInfo>());

                foreach (var entry in textureList)
                {
                    var mapping = new GameElementTemplateInfo(typeName, entry.Name, renderer.BindTexture(entry));
                    map[typeName].Add(mapping);
                }
            }
        }
    }
}
