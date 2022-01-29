using System.Numerics;
using ImGuiNET;

namespace Duality.Editing.Windows
{
    public class CreaturesEditingWindow
        : EditingWindow
    {
        public static TexturePointerMapping CurrentlySelected = TexturePointerMapping.Invalid;
        public override string Id => "Creatures Editing Window";

        protected override void PerformPreDraw(GameEditor editor)
        {
            IsEnabled = true;
            WindowFlags = ImGuiWindowFlags.NoTitleBar;
        }

        protected override void PerformDraw(GameEditor editor)
        {
            UpdateWindowRect(editor);

        }

        private static void UpdateWindowRect(GameEditor editor)
        {
            var viewport = editor.Driver.GraphicsDevice.Viewport;
            var viewportSize = new Vector2(viewport.Width, viewport.Height);
            var size = new Vector2(150.0f, 200.0f);
            var pos = new Vector2(viewportSize.X - size.X, 420.0f);
            ImGui.SetWindowPos(pos);
            ImGui.SetWindowSize(size);
        }

        private static void HandlePressed(TexturePointerMapping mapping)
        {
            CurrentlySelected = CurrentlySelected.Pointer == mapping.Pointer ? TexturePointerMapping.Invalid : mapping;
        }
    }
}
