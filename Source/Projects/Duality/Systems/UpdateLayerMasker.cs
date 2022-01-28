using Duality.Data;
using Duality.Editing.Windows;

namespace Duality.Systems
{
    public class UpdateLayerMasker
        : GameSystem
    {
        public override void Perform(GameDriver driver)
        {
            var currentLayer = TileEditingWindow.CurrentTileLayer;
            var masker = driver.LayerMasker;
            masker.Sprite.IsEnabled = currentLayer > 1 && TileEditingWindow.ShowLayerMask;
            masker.Sprite.ZIndex = currentLayer*GlobalConfiguration.SpriteLayerStep - 0.01f;
        }
    }
}
