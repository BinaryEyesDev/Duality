using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Registries
{
    using TextureMap = Dictionary<string, List<Texture2D>>;

    public class TextureRegistry
    {
        public readonly TextureMap Map = new();

        public TextureRegistry(GameDriver driver)
        {
            _driver = driver;
            LoadTextures("Tiles");
            LoadTextures("Objects");
            LoadTextures("Textures");
            //var objectDirectory = Path.Combine(contentDirectory, "Objects");
        }

        private void LoadTextures(string type)
        {
            Map.Add(type, new List<Texture2D>());

            var root = Directory.GetCurrentDirectory();
            var contentDirectory = Path.Combine(root, "Content");
            var tileDirectory = Path.Combine(contentDirectory, type);
            var filePaths = Directory.GetFiles(tileDirectory);
            foreach (var path in filePaths)
            {
                var contentPath = path.Remove(0, contentDirectory.Length + 1).Replace("\\", "/").Replace(".xnb", "");

                Log.Debug($"Loading: {contentPath}");
                Map[type].Add(_driver.Content.Load<Texture2D>(contentPath));
            }
        }

        private readonly GameDriver _driver;
    }
}
