using Duality.Editing.Utilities;
using Duality.Utilities;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGui.Standard;

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
            DrawTopMenu.Perform(this, driver);

            ImGui.Begin("MouseDataWindow");

            var mouse = Mouse.GetState();
            var mousePosition = new Vector2(mouse.X, mouse.Y);
            var worldPosition = Vector2.Transform(mousePosition, driver.MainCamera.Inverted);
            var gridPosition = CalculateGridFromWorld.Perform(worldPosition, new Vector2(64, 64));

            InspectVector2.Perform("Screen Position", mousePosition);
            InspectVector2.Perform("World Position", worldPosition);
            InspectVector2.Perform("Grid Position", gridPosition);

            ImGui.End();


            _renderer.EndLayout();
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
