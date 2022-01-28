using System.Numerics;
using Duality.Data;
using Duality.Utilities;
using ImGuiNET;

namespace Duality.Editing.Windows
{
    public class TileEditingWindow
        : EditingWindow
    {
        public static TexturePointerMapping CurrentlySelected = TexturePointerMapping.Invalid;
        public static int CurrentTileLayer = 1;
        public static float CameraZoomFactor = 1.0f;
        public static bool ShowAllLayers = true;
        public static bool ShowLayerMask = true;
        public static bool ShowGrid = true;
        public override string Id => "Tile Editing Window";

        protected override void PerformPreDraw(GameEditor editor)
        {
            IsEnabled = true;
            WindowFlags = ImGuiWindowFlags.NoTitleBar;
        }

        protected override void PerformDraw(GameEditor editor)
        {
            UpdateWindowRect(editor);

            ImGui.SliderInt("L##Tile Layer", ref CurrentTileLayer, 1, 5);
            ImGui.SliderFloat("Z##ZoomFactor", ref CameraZoomFactor, 0.1f, 2.0f);
            ImGui.Checkbox("Show All", ref ShowAllLayers);
            ImGui.Checkbox("Show Mask", ref ShowLayerMask);
            ImGui.Checkbox("Show Grid", ref ShowGrid);
            ImGui.Image(CurrentlySelected.Pointer, GlobalConfiguration.GuiTileSize);
            var icons = editor.TextureIcons;
            foreach (var (objectType, mappingList) in icons)
            {
                if (ImGui.TreeNode(objectType))
                {
                    foreach (var mapping in mappingList)
                    {
                        var pressed = ImGui.ImageButton(mapping.Pointer, GlobalConfiguration.GuiTileIconSize);
                        if (pressed)
                            HandlePressed(mapping);
                    }
                    
                    ImGui.TreePop();
                }
            }

            if (ImGui.Button("Fill Layer"))
            {
                if (!CurrentlySelected.IsValid) return;
                var texture = GameDriver.Instance.Editor.GetSelectedTileTexture();
                if (texture == null) return;

                var type = CurrentlySelected.TextureType;
                RunOnAllTiles.Perform((index, node) =>
                {
                    GameDriver.Instance.World.UpdateSpriteOnNode(index, texture, CurrentTileLayer, type);
                });
            }
        }

        private static void UpdateWindowRect(GameEditor editor)
        {
            var viewport = editor.Driver.GraphicsDevice.Viewport;
            var viewportSize = new Vector2(viewport.Width, viewport.Height);
            var size = new Vector2(150.0f, 400.0f);
            var pos = new Vector2(viewportSize.X - size.X, 30.0f);
            ImGui.SetWindowPos(pos);
            ImGui.SetWindowSize(size);
        }

        private static void HandlePressed(TexturePointerMapping mapping)
        {
            CurrentlySelected = CurrentlySelected.Pointer == mapping.Pointer ? TexturePointerMapping.Invalid : mapping;
        }
    }
}
