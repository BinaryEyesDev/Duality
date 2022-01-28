using ImGuiNET;

namespace Duality.Editing.Utilities
{
    public static class DrawTopMenu
    {
        public static void Perform(GameEditor editor, GameDriver driver)
        {
            ImGui.BeginMainMenuBar();
            if (ImGui.BeginMenu("File"))
            {
                if (ImGui.MenuItem("New"))
                {

                }

                if (ImGui.MenuItem("Quit"))
                    driver.Exit();

                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }
    }
}
