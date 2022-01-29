using Duality.Data;
using Duality.Editing;
using Duality.Utilities;
using Microsoft.Xna.Framework;

namespace Duality.Spawners
{
    public static class HumanSpawner
    {
        public static void SpawnCreature(TileEventArgs data)
        {
            var texture = GameDriver.Instance.Editor.GetSelectedTexture();
            if (texture == null) return;



            var layerId = GameViewManager.GetLayerId(data.Id.SubGroup);
            var sprite = GenerateSprite.Perform(texture);
            sprite.Transform.Position = CalculateGridFromWorld.GetWorldPosition(data.Index);
            sprite.Transform.Scale = Vector2.One*2.0f;
            sprite.Pivot = new Vector2(0.5f, 1.0f);
            sprite.ZIndex = GlobalConfiguration.GetZIndexCreatures(layerId);
        }
    }
}
