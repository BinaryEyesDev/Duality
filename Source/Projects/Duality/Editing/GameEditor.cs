using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public readonly struct GameElementTemplateInfo
    {
        public static GameElementTemplateInfo Invalid => new("", "", "", IntPtr.Zero);
        public readonly string GroupType;
        public readonly string SubGroupType;
        public readonly string TexturePath;
        public readonly IntPtr Pointer;
        public bool IsValid => Pointer != IntPtr.Zero;
        public string TextureName => Path.GetFileNameWithoutExtension(TexturePath);

        public GameElementTemplateInfo(
            string group,
            string subGroup, 
            string path, 
            IntPtr pointer)
        {
            GroupType = group;
            SubGroupType = subGroup;
            TexturePath = path;
            Pointer = pointer;
        }
    }
}
