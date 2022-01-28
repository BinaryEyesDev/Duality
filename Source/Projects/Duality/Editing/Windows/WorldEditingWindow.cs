using ImGuiNET;

namespace Duality.Editing.Windows
{
    public class WorldEditingWindow
        : EditingWindow
    {
        public override string Id => "World Editing Window";

        protected override void PerformDraw(GameEditor editor)
        {
            var driver = editor.Driver;
            ImGui.InputText("Name", ref driver.World.Name, 256);
        }
    }
}
