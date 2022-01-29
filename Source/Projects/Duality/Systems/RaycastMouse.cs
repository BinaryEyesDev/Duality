using System.Linq;
using Duality.Components;
using Duality.Data;
using Duality.Editing;
using Duality.Utilities;
using Microsoft.Xna.Framework;

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
                building?.FlipHorizontal();
                if (building != null) return;

                var grassTile = node.Layers.FirstOrDefault(entry => entry != null && entry.Type.Contains("Grass"));
                grassTile?.Rotate90();
            }

            if (MouseInput.WasButtonJustPressed(0))
            {
                if (driver.Editor.IsMouseCaptured) return;

                var info = TextureSelectionManager.CurrentlySelected;
                if (!info.IsValid) return;

                var groupType = info.GroupType;
                switch (groupType)
                {
                    case "Creatures": 
                        SpawnCreatures(info, gridIndex); 
                        break;

                    case "Tiles": 
                        SpawnTiles(info, gridIndex); 
                        break;
                }
            }
        }

        private void SpawnCreatures(GameElementTemplateInfo info, GridIndex gridIndex)
        {
            if (info.SubGroupType != "Humans") return;

            var worldPosition = CalculateGridFromWorld.GetWorldPosition(gridIndex);
            var layerId = GameViewManager.GetLayerId(info.SubGroupType);
            var texture = GameDriver.Instance.Editor.GetSelectedTexture();
            if (texture == null) return;

            //var sprite = GenerateSprite.Perform()
            var zIndex = GlobalConfiguration.GetZIndexCreatures(layerId);

        }

        private void SpawnTiles(GameElementTemplateInfo info, GridIndex gridIndex)
        {
            var driver = GameDriver.Instance;
            var subGroup = info.SubGroupType;
            var layerId = GameViewManager.GetLayerId(subGroup);

            var texture = driver.Editor.GetSelectedTexture();
            var shouldRemove = texture == null || texture == driver.World.GetCellSprite(gridIndex, layerId)?.Image;
            if (shouldRemove)
            {
                driver.World.RemoveSpriteFromNode(gridIndex, layerId);
                return;
            }

            driver.World.UpdateSpriteOnNode(gridIndex, texture, layerId, subGroup);
        }
    }
}
