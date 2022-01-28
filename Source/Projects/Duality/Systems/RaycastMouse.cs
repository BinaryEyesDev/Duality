using System;
using Duality.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Duality.Systems
{
    public class RaycastMouse
        : GameSystem
    {
        public override void Perform(GameDriver driver, GameTime time)
        {
            var mouse = Mouse.GetState();
            var mousePosition = mouse.Position.ToVector2();
            var worldPosition = Vector2.Transform(mousePosition, driver.MainCamera.Inverted);
            var gridPosition = CalculateGridFromWorld.Perform(worldPosition, new Vector2(64, 64));

            driver.EditorMouse.Transform.Position = gridPosition;
        }
    }
}
