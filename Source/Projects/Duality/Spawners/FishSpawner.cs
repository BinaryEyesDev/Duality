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
            if (waterTileCount < 8) return;

            var roll = GetRandom.Float(0.0f, 100.0f);
            if (roll < 75.0f) return;

            Log.Message("SpawningFish");
            GameDriver.Instance.MessageDisplay.AddMessage("Fish Spawned");

            var image = GameDriver.Instance.TextureRegistry.FindCreature("Water", "Fish_1");
            var sprite = GenerateSprite.Perform(GameDriver.Instance, image);
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

            var deathCount = GetRandom.Int32(10, CurrentPopulationCount);
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
