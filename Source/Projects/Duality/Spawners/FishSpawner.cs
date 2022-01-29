using System;
using System.Collections.Generic;
using Duality.Agents; 
using Duality.Data;
using Duality.Extensions;
using Duality.Utilities;
using Orca.Logging;

namespace Duality.Spawners
{
    public class FishSpawner
    {
        public static readonly List<Fish> Fishes = new();
        public static bool ExtinctionEventInProcess;
        public static int OverpopulationThreshold = 20;
        public static int CurrentPopulationCount => Fishes.Count;

        public static void SpawnCreature(TileEventArgs data)
        {
            if (ExtinctionEventInProcess) return;
            var waterTileCount = GameDriver.Instance.World.GetWaterTileCount();
            if (waterTileCount < 8)
            {
                GameDriver.Instance.MessageDisplay.AddDireMessage("Fish require 8 water tiles!");
                return;
            }

            var node = GameDriver.Instance.World[data.Index];
            var waterLayer = node.FindLowestLayerIndexFor(new GameObjectId("Tiles", "Water"));
            if (waterLayer == -1)
            {
                GameDriver.Instance.MessageDisplay.AddDireMessage("Can only spawn fish in water tiles!");
                return;
            }

            Log.Message("SpawningFish");
            GameDriver.Instance.MessageDisplay.AddMessage("Fish Spawned");

            var image = GameDriver.Instance.TextureRegistry.FindGameElementTemplateByName("Creatures", "Water", "Fish_1");
            var sprite = GenerateSprite.Perform(image);
            sprite.Transform.Position = data.WorldPosition;
            sprite.ZIndex = (data.LayerId*GlobalConfiguration.SpriteLayerStep) - GlobalConfiguration.SpriteLayerUnderStep;
            var fish = new Fish
            {
                Sprite = sprite,
            };

            Fishes.Add(fish);
            GameDriver.Instance.Agents.Add(fish);

            var overpopulationValue = CurrentPopulationCount - OverpopulationThreshold;
            if (overpopulationValue < 0) return;
            if (!_warnedAboutExtinction)
                WarnAboutExtinction();

            var extinctionChance = 100.0f - (overpopulationValue*2.0f);
            var extinctionEventRoll = GetRandom.Float(0.0f, 100.0f);
            if (extinctionEventRoll > extinctionChance)
                RunExtinctionEvent();
        }

        private static void WarnAboutExtinction()
        {
            _warnedAboutExtinction = true;
            GameDriver.Instance.MessageDisplay.AddDireMessage("Fish Extinction Event Imminent");
        }

        private static void RunExtinctionEvent()
        {
            Log.Message("Fish Extinction Event Registered");
            GameDriver.Instance.MessageDisplay.AddDireMessage("Fish Extinction Event Is Happening!");

            var deathRatio = GetRandom.Float(0.85f, 0.975f);
            var deathCount = Math.Floor(CurrentPopulationCount*deathRatio);
            while (deathCount > 0)
            {
                Fishes.PopRandom().State = AgentState.Dying;
                deathCount -= 1;
            }

            _warnedAboutExtinction = false;
        }

        private static bool _warnedAboutExtinction;
    }
}
