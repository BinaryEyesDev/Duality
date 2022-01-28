using System.Numerics;
using Duality.Data;
using Duality.Editing.Utilities;
using ImGuiNET;
using MonoGame.ImGui.Standard.Extensions;

namespace Duality.Editing
{
    public abstract class EditingWindow
    {
        public abstract string Id { get; }
        public bool IsEnabled { get; set; }
        public bool IsMouseHovering { get; private set; }
        protected abstract void PerformDraw(GameEditor editor);
        protected virtual void PerformPreDraw(GameEditor editor) { }
        public void ToggleEnabled() => IsEnabled = !IsEnabled;

        public void Draw(GameEditor editor)
        {
            PerformPreDraw(editor);
            if (!IsEnabled) return;
            if (!ImGui.Begin(Id, ImGuiWindowFlags.Modal)) return;

            var position = ImGui.GetWindowPos();
            var size = ImGui.GetWindowSize();
            var min = position - GlobalConfiguration.GuiMinWindowOffset;
            var max = min + size;
            IsMouseHovering = ImGui.IsMouseHoveringRect(min, max);
            //InspectWindowProperties(position, size, min, max);

            PerformDraw(editor);
            ImGui.End();
        }

        private void InspectWindowProperties(Vector2 position, Vector2 size, Vector2 min, Vector2 max)
        {
            ImGui.Separator();
            InspectVector2.Perform("Pos", position.ToXnaVector2());
            InspectVector2.Perform("Size", size.ToXnaVector2());
            InspectVector2.Perform("Min", min.ToXnaVector2());
            InspectVector2.Perform("Max", max.ToXnaVector2());
            InspectToggle.Perform("Is Mouse Hovering", IsMouseHovering);
            ImGui.Separator();
        }
    }
}
