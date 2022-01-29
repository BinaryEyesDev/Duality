using Duality.Data;
using Duality.Spawners;
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
            switch (args.Type)
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
            FishSpawner.SpawnCreature(tileData);
            AxolotlSpawner.SpawnCreature(tileData);
        }
    }
}
