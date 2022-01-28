using Duality.Editing.Windows;
using ImGuiNET;

namespace Duality.Editing.Utilities
{
    public static class DrawTopMenu
    {
        public static void Perform(GameEditor editor, GameDriver driver)
        {
            ImGui.BeginMainMenuBar();
            DrawFileMenu(driver);
            DrawWindowsMenu();

            ImGui.EndMainMenuBar();
        }

        private static void DrawWindowsMenu()
        {
            if (!ImGui.BeginMenu("Windows"))
                return;

            if (ImGui.MenuItem("Mouse Data Window"))
                MouseDataWindow.IsEnabled = !MouseDataWindow.IsEnabled;

            ImGui.EndMenu();
        }

        private static void DrawFileMenu(GameDriver driver)
        {
            if (!ImGui.BeginMenu("File")) 
                return;

            if (ImGui.MenuItem("New"))
            {
            }

            if (ImGui.MenuItem("Quit"))
                driver.Exit();

            ImGui.EndMenu();
        }
    }
}
