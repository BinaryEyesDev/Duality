using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Registries
{
    using TextureMap = Dictionary<string, List<Texture2D>>;
    using TileGroupingsMap = Dictionary<string, Dictionary<string, List<Texture2D>>>;

    public class TextureRegistry
    {
        public TileGroupingsMap Groupings;

        public Texture2D FindGameElementTemplateByPath(string group, string subGroup, string path)
        {
            var subGroupMap = FindSubGroup(group, subGroup);
            return subGroupMap.FirstOrDefault(entry => entry.Name == path);
        }

        public Texture2D FindGameElementTemplateByName(string group, string subGroup, string name)
        {
            var subGroupMap = FindSubGroup(group, subGroup);
            return subGroupMap.FirstOrDefault(entry => Path.GetFileNameWithoutExtension(entry.Name) == name);
        }

        private IEnumerable<Texture2D> FindSubGroup(string group, string subGroup)
        {
            var foundGroup = Groupings.TryGetValue(group, out var groupMap);
            if (!foundGroup)
            {
                Log.Warning($"failed to locate requested element group: {group}");
                return null;
            }

            var foundSubGroup = groupMap.TryGetValue(subGroup, out var subGroupMap);
            if (!foundSubGroup)
            {
                Log.Warning($"failed to locate requested element sub-group: {subGroupMap}");
                return null;
            }

            return subGroupMap;
        }

        public TextureRegistry()
        {
            Groupings = new TileGroupingsMap
            {
                {"Tiles", LoadTextures("Tiles")},
                {"Objects", LoadTextures("Objects")},
                {"Creatures", LoadTextures("Creatures")}
            };
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
    }
}
