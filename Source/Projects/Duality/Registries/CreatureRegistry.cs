using Duality.Agents;
using Duality.Data;
using Duality.Utilities;
using Orca.Logging;

namespace Duality.Registries
{
    public class CreatureRegistry
    {
        public CreatureRegistry()
        {
            GameDriver.Instance.World.OnTiledPlaced += HandleTilePlaced;
            _maxFish = GetRandom.Int32(15, 35);
        }

        private void HandleTilePlaced(object? sender, TileEventArgs args)
        {
            if (args.Type == "Water")
                SpawnFish(args);
        }

        private void SpawnFish(TileEventArgs tileData)
        {
            Log.Message("SpawningFish");
            if (_fish == _maxFish) return;

            var roll = GetRandom.Float(0.0f, 100.0f);
            if (roll < 80.0f) return;

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

        private int _maxFish;
        private int _fish;
    }
}
