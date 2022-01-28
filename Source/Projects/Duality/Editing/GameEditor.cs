using Duality.Editing.Utilities;
using Duality.Editing.Windows;
using Microsoft.Xna.Framework;
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
            MouseDataWindow.Draw(driver);

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
