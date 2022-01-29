using System;
using System.Collections.Generic;
using System.Linq;
using Duality.Data;
using Duality.Editing.Utilities;
using Duality.Editing.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.ImGui.Standard;

namespace Duality.Editing
{
    using GroupingInfoMap = Dictionary<string, Dictionary<string, List<GameElementTemplateInfo>>>;
    using SubGroupInfoMap = Dictionary<string, List<GameElementTemplateInfo>>;

    public class GameEditor
        : IDisposable
    {
        public GameDriver Driver { get; }
        public ImGUIRenderer Renderer { get; }
        public GroupingInfoMap IconMap { get; }
        public bool IsMouseCaptured => _windows.Values.Any(entry => entry.IsMouseHovering);

        public T GetEditingWindow<T>() where T : EditingWindow
        {
            var found = _windows.TryGetValue(typeof(T), out var window);
            return found ? (T) window : null;
        }

        public GameElementTemplateInfo GetSelectedElement()
        {
            return TextureSelectionManager.CurrentlySelected;
        }

        public Texture2D GetSelectedTileTexture()
        {
            var mapping = TextureSelectionManager.CurrentlySelected;
            return !mapping.IsValid ? null : Driver.TextureRegistry.FindGameElementTemplateByPath("Tiles", mapping.SubGroupType, mapping.TexturePath);
        }

        public void Dispose()
        {
            foreach (var groupEntry in IconMap)
            {
                foreach (var subGroupEntry in groupEntry.Value)
                {
                    foreach (var entry in subGroupEntry.Value)
                        Renderer.UnbindTexture(entry.Pointer);
                }
            }
        }

        public void Update(GameDriver driver, GameTime time)
        {
            
        }

        public void Draw(GameTime time)
        {
            Renderer.BeginLayout(time);
            DrawTopMenu.Perform(this);
            foreach (var window in _windows.Values)
                window.Draw(this);

            Renderer.EndLayout();
        }

        public GameEditor(GameDriver driver)
        {
            Driver = driver;
            Renderer = new ImGUIRenderer(driver).Initialize().RebuildFontAtlas();
            IconMap = LoadTextureIcons.Perform(driver, Renderer);
            AddWindow<MouseDataWindow>();
            AddWindow<WorldEditingWindow>();
            AddWindow<SelectedPreviewWindow>();
            AddWindow<TileEditingWindow>();
            AddWindow<CreaturesEditingWindow>();
        }

        private void AddWindow<T>() where T : EditingWindow, new()
        {
            var window = new T();
            _windows.Add(typeof(T), window);
        }

        private readonly Dictionary<Type, EditingWindow> _windows = new();
    }
}
