using Duality.Data;

namespace Duality.Extensions
{
    public static class WorldExtensions
    {
        public static int FindLowestLayerIndexFor(this GameNode node, GameObjectId id)
        {
            for (var i = 0; i < node.Layers.Length; i++)
            {
                var layer = node.Layers[i];
                if (layer == null) continue;
                if (layer.Id.Group == id.Group && layer.Id.SubGroup == id.SubGroup)
                    return i;
            }

            return -1;
        }
    }
}
