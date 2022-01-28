using ImGuiNET;

namespace Duality.Editing.Windows
{
    public static class TextureRegistryWindow
    {
        public static bool IsEnabled;

        public static void Draw(GameEditor editor)
        {
            if (!IsEnabled)
                return;

            if (!ImGui.Begin("Texture Registry Window"))
                return;

            var map = editor.Driver.TextureRegistry.Map;
            foreach (var (typeName, textureList) in map)
            {
                if (ImGui.TreeNode(typeName))
                {
                    foreach (var texture in textureList)
                    {
                        
                    }
                    ImGui.TreePop();
                }
            }

            ImGui.End();
        }
    }
}
