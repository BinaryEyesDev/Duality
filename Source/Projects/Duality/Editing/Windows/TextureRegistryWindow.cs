using System;
using Duality.Data;
using ImGuiNET;

namespace Duality.Editing.Windows
{
    public static class TextureRegistryWindow
    {
        public static bool IsEnabled;
        public static TexturePointerMapping CurrentlySelected = TexturePointerMapping.Invalid;

        public static void Draw(GameEditor editor)
        {
            if (!IsEnabled)
                return;

            if (!ImGui.Begin("Texture Registry Window"))
                return;
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

            ImGui.End();
        }

        private static void HandlePressed(TexturePointerMapping mapping)
        {
            CurrentlySelected = CurrentlySelected.Pointer == mapping.Pointer ? TexturePointerMapping.Invalid : mapping;
        }
    }
}
