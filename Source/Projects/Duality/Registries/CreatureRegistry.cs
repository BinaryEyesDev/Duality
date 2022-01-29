using Duality.Data;
using Duality.Spawners;
using Duality.Utilities;
using Orca.Logging;

namespace Duality.Registries
{
    public class CreatureRegistry
    {
        public CreatureRegistry()
        {
            GameDriver.Instance.World.OnLayerObjectAdded += HandleTilePlaced;
        }

        private void HandleTilePlaced(object? sender, TileEventArgs args)
        {
            switch (args.Id.SubGroup)
            {
                case "Water": SpawnWaterCreature(args); break;
                case "Grass": SpawnGrassyCreature(args); break;
            }
        }

        private void SpawnGrassyCreature(TileEventArgs tileData)
        {
            Log.Message("SpawningGrassyCreature");

        }

        private void SpawnWaterCreature(TileEventArgs tileData)
        {
            Log.Message("SpawningWaterCreature");
            var roll = GetRandom.Float(0.0f, 100.0f);
            if (roll > 98.0f)
                FishSpawner.SpawnCreature(tileData);
            else if (roll > 99.0f)
                AxolotlSpawner.SpawnCreature(tileData);
        }
    }
}
