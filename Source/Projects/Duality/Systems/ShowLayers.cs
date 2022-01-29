using Duality.Data;
using Duality.Editing;
using Duality.Editing.Windows;

namespace Duality.Systems
{
    public class ShowLayers
        : GameSystem
    {
        public override void Perform(GameDriver driver)
        {
            if (_wasShowing == GameViewManager.ShowAllLayers && _layerId == GameViewManager.CurrentTileLayer)
                return;

            for (var row = 0; row < GlobalConfiguration.MapSize.X; row++)
            {
                for (var col = 0; col < GlobalConfiguration.MapSize.Y; col++)
                {
                    var index = new GridIndex(col, row);
                    var node = driver.World.GetNode(index);
                    if (node == null) continue;

                    for (var i = 0; i < node.Layers.Length; i++)
                    {
                        if (node.Layers[i] == null) continue;
                        node.Layers[i].IsEnabled = CheckLayerVisibility(i);
                    }
                }
            }

            _layerId = GameViewManager.CurrentTileLayer;
            _wasShowing = GameViewManager.ShowAllLayers;
        }

        private static bool CheckLayerVisibility(int layerIndex)
        {
            if (GameViewManager.ShowAllLayers)
                return true;

            var layerId = layerIndex + 1;
            return layerId == GameViewManager.CurrentTileLayer;
        }

        private int _layerId;
        private bool _wasShowing;
    }
}
