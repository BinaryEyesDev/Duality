using System.Collections.Generic;
using Duality.Agents;
using Duality.Data;
using Orca.Logging;

namespace Duality.Spawners
{
    public static class AxototlSpawner
    {
        public static readonly List<Axolotl> Axototls = new();

        public static void SpawnCreature(TileEventArgs data)
        {
            Log.Message("Spawning Axototl");
        }
    }
}
