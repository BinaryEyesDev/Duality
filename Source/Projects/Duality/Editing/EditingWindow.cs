using Duality.Editing.Utilities;
using ImGuiNET;
using Microsoft.Xna.Framework;
using MonoGame.ImGui.Standard.Extensions;

namespace Duality.Editing
{
    public abstract class EditingWindow
    {
        public abstract string Id { get; }
        public bool IsEnabled { get; set; }
        public bool IsMouseHovering { get; private set; }
        protected abstract void PerformDraw(GameEditor editor);

        public void ToggleEnabled()
        {
            IsEnabled = !IsEnabled;
        }

        public void Draw(GameEditor editor)
        {
            if (!IsEnabled) return;
            if (!ImGui.Begin(Id)) return;

            var position = ImGui.GetWindowPos();
            var size = ImGui.GetWindowSize();
            var max = position + size;
            IsMouseHovering = ImGui.IsMouseHoveringRect(position, max);

            PerformDraw(editor);
            ImGui.End();
        }
    }
}
