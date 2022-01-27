using ImGuiNET;
using Microsoft.Xna.Framework;
using MonoGame.ImGui.Standard;
using Orca.Logging;

namespace Duality.Editing
{
    public class GameEditor
    {
        public void Update(GameDriver driver, GameTime time)
        {
            
        }

        public void Draw(GameDriver driver, GameTime time)
        {
            _renderer.BeginLayout(time);
            DrawTopMenu(driver);

            _renderer.EndLayout();
        }

        private void DrawTopMenu(GameDriver driver)
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

        public GameEditor(GameDriver driver)
        {
            _renderer = new ImGUIRenderer(driver)
                .Initialize()
                .RebuildFontAtlas();
        }

        private readonly ImGUIRenderer _renderer;
    }
}
