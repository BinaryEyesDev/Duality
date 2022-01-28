using System.Numerics;
using Duality.Data;
using ImGuiNET;

namespace Duality.Editing.Windows
{
    public class TextureRegistryWindow
        : EditingWindow
    {
        public static TexturePointerMapping CurrentlySelected = TexturePointerMapping.Invalid;
        public override string Id => "Texture Registry Window";

        protected override void PerformDraw(GameEditor editor)
        {
            var w = ImGui.GetWindowWidth();
            var h = ImGui.GetWindowHeight();
            var size = new Vector2(w, h);
            ImGui.InputFloat2("Rect", ref size);

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

        private static void HandlePressed(TexturePointerMapping mapping)
        {
            CurrentlySelected = CurrentlySelected.Pointer == mapping.Pointer ? TexturePointerMapping.Invalid : mapping;
        }
    }
}
