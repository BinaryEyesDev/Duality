using System;
using System.Globalization;
using Duality.Data;
using ImGuiNET;
using Vector2 = System.Numerics.Vector2;

namespace Duality.Editing.Windows
{
    public static class TextureRegistryWindow
    {
        public static bool IsEnabled;
        public static IntPtr CurrentlySelected = IntPtr.Zero;

        public static void Draw(GameEditor editor)
        {
            if (!IsEnabled)
                return;

            if (!ImGui.Begin("Texture Registry Window"))
                return;
            ImGui.Image(CurrentlySelected, GlobalConfiguration.GuiTileSize);

            var icons = editor.TextureIcons;
            foreach (var (objectType, mappingList) in icons)
            {
                if (ImGui.TreeNode(objectType))
                {
                    foreach (var mapping in mappingList)
                    {
                        var pressed = ImGui.ImageButton(mapping.Pointer, GlobalConfiguration.GuiTileIconSize);
                        if (pressed)
                            HandlePressed(mapping.Pointer);
                    }
                    
                    ImGui.TreePop();
                }
            }

            ImGui.End();
        }

        private static void HandlePressed(IntPtr texturePtr)
        {
            if (CurrentlySelected == texturePtr)
                CurrentlySelected = IntPtr.Zero;
            else
                CurrentlySelected = texturePtr;
        }
    }
}
