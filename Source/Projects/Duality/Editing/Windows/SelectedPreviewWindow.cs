using System.Numerics;
using Duality.Data;
using ImGuiNET;

namespace Duality.Editing.Windows
{
    public class SelectedPreviewWindow
        : EditingWindow
    {
        public override string Id => "Selected Preview Window";

        protected override void PerformPreDraw(GameEditor editor)
        {
            IsEnabled = true;
            WindowFlags = ImGuiWindowFlags.NoTitleBar;
        }

        protected override void PerformDraw(GameEditor editor)
        {
            UpdateWindowRect(editor);

            ImGui.SliderInt("L##Tile Layer", ref GameViewManager.CurrentTileLayer, 1, GlobalConfiguration.LayerCount);
            ImGui.SliderFloat("Z##ZoomFactor", ref GameViewManager.CameraZoomFactor, GlobalConfiguration.MinimumCameraZoomFactor, GlobalConfiguration.MaximumCameraZoomFactor);
            GameViewManager.SetZoomFactory(GameViewManager.CameraZoomFactor);

            ImGui.Checkbox("Show All", ref GameViewManager.ShowAllLayers);
            ImGui.Checkbox("Show Mask", ref GameViewManager.ShowLayerMask);
            ImGui.Checkbox("Show Grid", ref GameViewManager.ShowGrid);
            ImGui.Image(TextureSelectionManager.CurrentlySelected.Pointer, GlobalConfiguration.GuiTileSize);
        }

        private static void UpdateWindowRect(GameEditor editor)
        {
            var viewport = editor.Driver.GraphicsDevice.Viewport;
            var viewportSize = new Vector2(viewport.Width, viewport.Height);
            var size = new Vector2(150.0f, 200.0f);
            var pos = new Vector2(viewportSize.X - size.X, 20.0f);
            ImGui.SetWindowPos(pos);
            ImGui.SetWindowSize(size);
        }
    }
}
