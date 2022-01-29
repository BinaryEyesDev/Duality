using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Registries
{
    using TextureMap = Dictionary<string, List<Texture2D>>;
    using TileGroupingsMap = Dictionary<string, Dictionary<string, List<Texture2D>>>;

    public class TextureRegistry
    {
        public TileGroupingsMap Groupings;
        public readonly TextureMap Tiles;
        public readonly TextureMap Creatures;
        public readonly TextureMap Objects;

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

        private List<Texture2D> FindSubGroup(string group, string subGroup)
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

        //public Texture2D FindCreature(string type, string name)
        //{
        //    var typeFound = Creatures.TryGetValue(type, out var textures);
        //    return typeFound ? textures.FirstOrDefault(entry => Path.GetFileNameWithoutExtension(entry.Name) == name) : null;
        //}

        //public Texture2D FindObject(string type, string name)
        //{
        //    var typeFound = Objects.TryGetValue(type, out var textures);
        //    return typeFound ? textures.FirstOrDefault(entry => entry.Name == name) : null;
        //}

        //public Texture2D FindTile(string type, string name)
        //{
        //    var typeFound = Tiles.TryGetValue(type, out var textures);
        //    return typeFound ? textures.FirstOrDefault(entry => entry.Name == name) : null;
        //}

        public TextureRegistry(GameDriver driver)
        {
            _driver = driver;
            Groupings = new TileGroupingsMap
            {
                {"Tiles", LoadTextures("Tiles")},
                {"Objects", LoadTextures("Objects")},
                {"Creatures", LoadTextures("Creatures")}
            };

            //Tiles = LoadTextures("Tiles");
            //Creatures = LoadTextures("Creatures");
            //Objects = LoadTextures("Objects");
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
