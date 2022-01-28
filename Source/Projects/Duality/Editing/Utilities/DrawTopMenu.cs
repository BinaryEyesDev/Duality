using Duality.Editing.Windows;
using ImGuiNET;

namespace Duality.Editing.Utilities
{
    public static class DrawTopMenu
    {
        public static void Perform(GameEditor editor)
        {
            ImGui.BeginMainMenuBar();

            var driver = editor.Driver;
            DrawFileMenu(driver);
            DrawWindowsMenu(editor);

            ImGui.EndMainMenuBar();
        }

        private static void DrawWindowsMenu(GameEditor editor)
        {
            if (!ImGui.BeginMenu("Windows"))
                return;

            if (ImGui.MenuItem("Mouse Data Window"))
                editor.GetEditingWindow<MouseDataWindow>().ToggleEnabled();

            if (ImGui.MenuItem("World Editing Window"))
                editor.GetEditingWindow<WorldEditingWindow>().ToggleEnabled();

            if (ImGui.MenuItem("Texture Registry Window"))
                editor.GetEditingWindow<TextureRegistryWindow>().ToggleEnabled();

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
