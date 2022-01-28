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

        protected override void PerformPreDraw(GameEditor editor)
        {
            IsEnabled = true;
            var viewport = editor.Driver.GraphicsDevice.Viewport;
            var viewportSize = new Vector2(viewport.Width, viewport.Height);
            var size = new Vector2(100.0f, 400.0f);
            var pos = new Vector2(viewportSize.X - size.X, 20.0f);
            ImGui.SetWindowPos(pos);
            ImGui.SetWindowSize(size);
        }

        protected override void PerformDraw(GameEditor editor)
        {
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
