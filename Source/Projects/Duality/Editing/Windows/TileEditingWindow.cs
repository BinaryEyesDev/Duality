using System.Numerics;
using Duality.Data;
using ImGuiNET;

namespace Duality.Editing.Windows
{
    public class TileEditingWindow
        : EditingWindow
    {
        public static TexturePointerMapping CurrentlySelected = TexturePointerMapping.Invalid;
        public static int CurrentTileLayer = 1;
        public static bool ShowAllLayers = true;
        public override string Id => "Tile Editing Window";

        protected override void PerformPreDraw(GameEditor editor)
        {
            IsEnabled = true;
            WindowFlags = ImGuiWindowFlags.Modal | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar;
        }

        protected override void PerformDraw(GameEditor editor)
        {
            UpdateWindowRect(editor);

            ImGui.SliderInt("##Tile Layer", ref CurrentTileLayer, 1, 5);
            ImGui.Checkbox("All Layers", ref ShowAllLayers);

            ImGui.Image(CurrentlySelected.Pointer, GlobalConfiguration.GuiTileSize);
            var icons = editor.TextureIcons;
            foreach (var (objectType, mappingList) in icons)
            {
                if (ImGui.TreeNode(objectType))
                {
                    foreach (var mapping in mappingList)
                    {
                        var pressed = ImGui.ImageButton(mapping.Pointer, GlobalConfiguration.GuiTileIconSize);
                        if (pressed)
                            HandlePressed(mapping);
                    }
                    
                    ImGui.TreePop();
                }
            }
        }

        private static void UpdateWindowRect(GameEditor editor)
        {
            var viewport = editor.Driver.GraphicsDevice.Viewport;
            var viewportSize = new Vector2(viewport.Width, viewport.Height);
            var size = new Vector2(100.0f, 400.0f);
            var pos = new Vector2(viewportSize.X - size.X, 20.0f);
            ImGui.SetWindowPos(pos);
            ImGui.SetWindowSize(size);
        }

        private static void HandlePressed(TexturePointerMapping mapping)
        {
            CurrentlySelected = CurrentlySelected.Pointer == mapping.Pointer ? TexturePointerMapping.Invalid : mapping;
        }
    }
}
