using Duality.Editing.Utilities;
using Duality.Editing.Windows;
using Microsoft.Xna.Framework;
using MonoGame.ImGui.Standard;

namespace Duality.Editing
{
    public class GameEditor
    {
        public GameDriver Driver { get; private set; }

        public void Update(GameDriver driver, GameTime time)
        {
            
        }

        public void Draw(GameTime time)
        {
            _renderer.BeginLayout(time);
            DrawTopMenu.Perform(this);
            MouseDataWindow.Draw(this);

            _renderer.EndLayout();
        }

        public GameEditor(GameDriver driver)
        {
            Driver = driver;
            _renderer = new ImGUIRenderer(driver)
                .Initialize()
                .RebuildFontAtlas();
        }

        private readonly ImGUIRenderer _renderer;
    }
}
