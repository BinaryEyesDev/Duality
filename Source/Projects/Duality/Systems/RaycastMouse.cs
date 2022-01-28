using Duality.Components;
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
                if (driver.Editor.IsMouseCaptured)
                    return;

                var texture = driver.Editor.GetSelectedTileTexture();
                if (texture == null) 
                    return;

                var sprite = GenerateSprite.Perform(driver, texture);
                sprite.Transform.Position = gridPosition;
            }
        }
    }
}
