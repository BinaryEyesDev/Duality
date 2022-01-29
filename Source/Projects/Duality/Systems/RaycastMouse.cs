using System.Linq;
using Duality.Components;
using Duality.Editing.Windows;
using Duality.Utilities;
using Microsoft.Xna.Framework;
using Orca.Logging;

namespace Duality.Systems
{
    public class RaycastMouse
        : GameSystem
    {
        public override void Perform(GameDriver driver)
        {
            var mousePosition = MouseInput.GetScreenPosition();
            var worldPosition = Vector2.Transform(mousePosition, driver.MainCamera.Inverted);
            var gridPosition = CalculateGridFromWorld.GetGridWorldPosition(worldPosition);
            var gridIndex = CalculateGridFromWorld.GetGridIndex(gridPosition);

            driver.EditorMouse.Transform.Position = gridPosition;
            if (MouseInput.WasButtonJustPressed(1))
            {
                var node = driver.World[gridIndex];
                var building = node.Layers.FirstOrDefault(entry => entry != null && entry.Type.Contains("Building"));
                if (building != null)
                    building.FlipHorizontal();
            }

            if (MouseInput.WasButtonJustPressed(0))
            {
                if (driver.Editor.IsMouseCaptured) return;
                if (!driver.Editor.GetSelectedElement().IsValid) return;
                
                var type = driver.Editor.GetSelectedElement().TextureType;
                TileEditingWindow.CurrentTileLayer = DetermineTypeLayerId(type);

                var layerId = TileEditingWindow.CurrentTileLayer;
                var texture = driver.Editor.GetSelectedTileTexture();
                var shouldRemove = texture == null || texture == driver.World.GetCellSprite(gridIndex, layerId)?.Image;
                if (shouldRemove)
                {
                    driver.World.RemoveSpriteFromNode(gridIndex, layerId);
                    return;
                }
                
                driver.World.UpdateSpriteOnNode(gridIndex, texture, layerId, type);
            }
        }

        private int DetermineTypeLayerId(string type)
        {
            switch (type)
            {
                case "Water": return 1;
                case "Grass": return 2;
                case "Nature": return 3;
                case "Buildings": return 4;
                default: return TileEditingWindow.CurrentTileLayer;
            }
        }
    }
}
