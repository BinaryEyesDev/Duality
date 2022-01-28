﻿using Duality.Components;
using Duality.Editing.Windows;
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
            
            driver.EditorMouse.Transform.Position = gridPosition;
            if (MouseInput.WasButtonJustPressed(0))
            {
                if (driver.Editor.IsMouseCaptured) return;
                if (!driver.Editor.GetCurrentlyMappedTile().IsValid) return;

                var gridIndex = CalculateGridFromWorld.GetGridIndex(gridPosition);
                var type = driver.Editor.GetCurrentlyMappedTile().TextureType;
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
                default: return TileEditingWindow.CurrentTileLayer;
            }
        }
    }
}
