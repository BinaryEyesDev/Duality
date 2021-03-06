using System.Numerics;
using Duality.Data;
using ImGuiNET;

namespace Duality.Editing.Windows
{
    public class CreaturesEditingWindow
        : EditingWindow
    {
        public override string Id => "Creatures Editing Window";

        protected override void PerformPreDraw(GameEditor editor)
        {
            IsEnabled = true;
            WindowFlags = ImGuiWindowFlags.NoTitleBar;
        }

        protected override void PerformDraw(GameEditor editor)
        {
            UpdateWindowRect(editor);
            var iconMap = editor.IconMap;
            foreach (var groupEntry in iconMap)
            {
                if (groupEntry.Key != "Creatures") continue;
                foreach (var subGroupEntry in groupEntry.Value)
                {
                    if (!ImGui.TreeNode(subGroupEntry.Key)) continue;
                    foreach (var entry in subGroupEntry.Value)
                    {
                        var pressed = ImGui.ImageButton(entry.Pointer, GlobalConfiguration.GuiTileIconSize);
                        if (!pressed) continue;
                        TextureSelectionManager.Set(entry);
                    }
                    ImGui.TreePop();
                }
            }

            if (ImGui.ImageButton(editor.BoomButtonIconPointer, new Vector2(125.0f, 125.0f)))
                DestructionManager.Start();
        }

        private static void UpdateWindowRect(GameEditor editor)
        {
            var viewport = editor.Driver.GraphicsDevice.Viewport;
            var viewportSize = new Vector2(viewport.Width, viewport.Height);
            var size = new Vector2(150.0f, 200.0f);
            var pos = new Vector2(viewportSize.X - size.X, 420.0f);
            ImGui.SetWindowPos(pos);
            ImGui.SetWindowSize(size);
        }
    }
}
