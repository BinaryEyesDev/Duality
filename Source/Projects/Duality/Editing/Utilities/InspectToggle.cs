using ImGuiNET;

namespace Duality.Editing.Utilities
{
    public static class InspectToggle
    {
        public static bool Perform(string label, bool value)
        {
            ImGui.Checkbox(label, ref value);
            return value;
        }
    }
}
