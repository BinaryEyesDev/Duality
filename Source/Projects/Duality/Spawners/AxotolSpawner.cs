using System.Collections.Generic;
using System.Linq;
using Duality.Agents;
using Duality.Data;
using Duality.Utilities;
using Orca.Logging;

namespace Duality.Spawners
{
    public static class AxolotlSpawner
    {
        public static readonly List<Axolotl> Axolotls = new();
        public static int CurrentPopulationCount => Axolotls.Count;

        public static void SpawnCreature(TileEventArgs data)
        {
            if (!ValidateNearGrassEdge(data))
                return;

            if (CurrentPopulationCount == 4) 
                return;

            var roll = GetRandom.Float(0.0f, 100.0f);
            if (roll < 95.0f) return;

            Log.Message("Spawning Axolotl");
            GameDriver.Instance.MessageDisplay.AddMessage("Spawning Axototl");

            var image = GameDriver.Instance.TextureRegistry.FindGameElementTemplateByName("Creatures", "Water", "Axolotl_1");

            var sprite = GenerateSprite.Perform(image);
            sprite.Transform.Position = data.WorldPosition;
            sprite.ZIndex = (data.LayerId * GlobalConfiguration.SpriteLayerStep) - GlobalConfiguration.SpriteLayerUnderStep;
            var fish = new Axolotl
            {
                Sprite = sprite,
            };

            Axolotls.Add(fish);
            GameDriver.Instance.Agents.Add(fish);
        }

        private static bool ValidateNearGrassEdge(TileEventArgs data)
        {
            var cell = GameDriver.Instance.World[data.Index];
            if (cell.Layers.Any(entry => entry is {Type: "Grass"}))
                return false;

            var validCount = 0;
            var adjacentInfos = GenerateAdjacentDirectionInfos.Perform(data.Index);
            foreach (var info in adjacentInfos)
            {
                var node = GameDriver.Instance.World[info.Index];
                var grass = node.Layers.FirstOrDefault(entry => entry is { Type: "Grass" });
                if (grass == null) continue;
                if (grass.Image.Name.Contains("5")) continue;
                validCount += 1;
            }

            return validCount > 0;
        }
    }
}
