using System;
using System.Reflection.Metadata.Ecma335;
using Duality.Agents;
using Duality.Data;
using Duality.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Registries
{
    public class CreatureRegistry
    {
        public CreatureRegistry()
        {
            GameDriver.Instance.World.OnTiledPlaced += HandleTilePlaced;
        }

        private void HandleTilePlaced(object? sender, TileEventArgs args)
        {
            if (args.Type == "Water")
                SpawnFish(args);
        }

        private void SpawnFish(TileEventArgs tileData)
        {
            Log.Message("SpawningFish");
            if (_fish == 1) return;

            //var roll = GetRandom.Float(0.0f, 100.0f);
            //if (roll < 75.0f) return;

            _fish += 1;
            var image = GameDriver.Instance.TextureRegistry.FindCreature("Water", "Fish_1");
            var sprite = GenerateSprite.Perform(GameDriver.Instance, image);
            sprite.Transform.Position = tileData.WorldPosition;
            sprite.ZIndex = (tileData.LayerId*GlobalConfiguration.SpriteLayerStep) - 0.01f;

            var fish = new Fish
            {
                Sprite = sprite,
            };

            GameDriver.Instance.Agents.Add(fish);
        }

        private int _fish;
    }
}
