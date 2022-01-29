using System.ComponentModel;
using System.Linq;
using Duality.Components;
using Duality.Data;
using Duality.Editing;
using Duality.Spawners;
using Duality.Utilities;
using ImGuiNET;
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
                for (var i = node.Layers.Length - 1; i >= 0; i--)
                {
                    if (node.Layers[i] == null) continue;
                    if (node.Layers[i].Id.Group == "Creatures") continue;

                    if (node.Layers[i].Id.Group == "Buildings")
                        node.Layers[i].FlipHorizontal();
                    else
                        node.Layers[i].Rotate90();

                    break;
                }
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
                    case "Objects":
                        SpawnTiles(info, gridIndex); 
                        break;
                }
            }
        }

        private static void SpawnCreatures(GameElementTemplateInfo info, GridIndex gridIndex)
        {
            var spawnData = new TileEventArgs(info.GetId(), gridIndex);
            var animalType = GenerateCreatureType.FromTextureName(info);
            switch (animalType)
            {
                case "Fish": FishSpawner.SpawnCreature(spawnData); return;
                case "Turtle": TurtleSpawner.SpawnCreature(spawnData); return;
                case "Axolotl": AxolotlSpawner.SpawnCreature(spawnData); return;
                case "Human": HumanSpawner.SpawnCreature(spawnData); return;
            }
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

            driver.World.UpdateSpriteOnNode(gridIndex, texture, layerId, info.GetId());
        }
    }
}
