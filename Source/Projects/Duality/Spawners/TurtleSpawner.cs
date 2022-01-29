using System;
using System.Collections.Generic;
using Duality.Agents;
using Duality.Data;
using Duality.Extensions;
using Duality.Utilities;
using Orca.Logging;

namespace Duality.Spawners
{
    public class TurtleSpawner
    {
        public static readonly List<Turtle> Turtles = new();
        public static bool ExtinctionEventInProcess;
        public static int OverpopulationThreshold = 10;
        public static int CurrentPopulationCount => Turtles.Count;

        public static void SpawnCreature(TileEventArgs data)
        {
            if (ExtinctionEventInProcess) return;
            var waterTileCount = GameDriver.Instance.World.GetWaterTileCount();
            if (waterTileCount < 16)
            {
                GameDriver.Instance.MessageDisplay.AddDireMessage("Turtle require 16 water tiles!");
                return;
            }

            var node = GameDriver.Instance.World[data.Index];
            var waterLayer = node.FindLowestLayerIndexFor(new GameObjectId("Tiles", "Water"));
            if (waterLayer == -1)
            {
                GameDriver.Instance.MessageDisplay.AddDireMessage("Can only spawn Turtle in water tiles!");
                return;
            }

            Log.Message("SpawningTurtle");
            GameDriver.Instance.MessageDisplay.AddMessage("Turtle Spawned");

            var image = GameDriver.Instance.TextureRegistry.FindGameElementTemplateByName("Creatures", "Water", "Turtle_1");
            var sprite = GenerateSprite.Perform(image);
            sprite.Transform.Position = data.WorldPosition;
            sprite.ZIndex = (data.LayerId * GlobalConfiguration.SpriteLayerStep) - GlobalConfiguration.SpriteLayerUnderStep;
            var turtle = new Turtle
            {
                Sprite = sprite,
            };

            Turtles.Add(turtle);
            GameDriver.Instance.Agents.Add(turtle);

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
            GameDriver.Instance.MessageDisplay.AddDireMessage("Turtle Extinction Event Imminent");
        }

        private static void RunExtinctionEvent()
        {
            Log.Message("Turtle Extinction Event Registered");
            GameDriver.Instance.MessageDisplay.AddDireMessage("Turtle Extinction Event Is Happening!");

            var deathRatio = GetRandom.Float(0.85f, 0.975f);
            var deathCount = Math.Floor(CurrentPopulationCount * deathRatio);
            while (deathCount > 0)
            {
                Turtles.PopRandom().State = AgentState.Dying;
                deathCount -= 1;
            }

            _warnedAboutExtinction = false;
        }

        private static bool _warnedAboutExtinction;
    }
}
