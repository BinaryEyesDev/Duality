using Duality.Editing.Utilities;
using Duality.Extensions;
using Duality.Utilities;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Duality.Editing.Windows
{
    public static class MouseDataWindow
    {
        public static bool IsEnabled = false;

        public static void Draw(GameEditor editor)
        {
            if (!IsEnabled) return;
            ImGui.Begin("MouseDataWindow");
            
            var driver = editor.Driver;
            var mouse = Mouse.GetState();
            var mousePosition = new Vector2(mouse.X, mouse.Y);
            var worldPosition = Vector2.Transform(mousePosition, driver.MainCamera.Inverted);
            var gridWorldPosition = CalculateGridFromWorld.GetGridWorldPosition(worldPosition);
            var gridIndex = CalculateGridFromWorld.GetGridIndex(gridWorldPosition).ToXnaVector2();

            InspectVector2.Perform("Screen Position", mousePosition);
            InspectVector2.Perform("World Position", worldPosition);
            InspectVector2.Perform("Grid Position", gridWorldPosition);
            InspectVector2.Perform("Grid Index", gridIndex);

            ImGui.End();
        }
    }
}
