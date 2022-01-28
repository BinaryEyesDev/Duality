using ImGuiNET;

namespace Duality.Editing.Windows
{
    public static class WorldEditingWindow
    {
        public static bool IsEnabled;

        public static void Draw(GameEditor editor)
        {
            if (!IsEnabled)
                return;

            if (!ImGui.Begin("World Editing Window"))
                return;

            var driver = editor.Driver;
            ImGui.InputText("Name", ref driver.World.Name, 256);

            ImGui.End();
        }
    }
}
