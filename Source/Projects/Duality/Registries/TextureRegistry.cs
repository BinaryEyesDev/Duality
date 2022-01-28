using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Registries
{
    using TextureMap = Dictionary<string, List<Texture2D>>;

    public class TextureRegistry
    {
        public readonly TextureMap Tiles;

        public Texture2D FindTile(string type, string name)
        {
            var typeFound = Tiles.TryGetValue(type, out var textures);
            return typeFound ? textures.FirstOrDefault(entry => entry.Name == name) : null;
        }

        public TextureRegistry(GameDriver driver)
        {
            _driver = driver;
            Tiles = LoadTextures("Tiles");
            //LoadTextures("Objects");
        }

        private static TextureMap LoadTextures(string objectType)
        {
            var map = new TextureMap();
            
            var root = Directory.GetCurrentDirectory();
            var contentDirectory = Path.Combine(root, "Content");
            var directory = Path.Combine(contentDirectory, objectType);
            var typeDirectories = Directory.GetDirectories(directory);
            foreach (var typeDirectory in typeDirectories)
            {
                var type = Path.GetFileNameWithoutExtension(typeDirectory);
                Log.Debug($"DiscoveredType: Type={type} Group={objectType}");

                map.Add(type, new List<Texture2D>());
                var files = Directory.GetFiles(typeDirectory);
                foreach (var path in files)
                {
                    var contentPath = path.Remove(0, contentDirectory.Length + 1).Replace("\\", "/").Replace(".xnb", "");
                    Log.Message($"LoadingTypeImage: {contentPath}");

                    map[type].Add(GameDriver.Instance.Content.Load<Texture2D>(contentPath));
                }
            }

            return map;
        }

        private readonly GameDriver _driver;
    }
}
