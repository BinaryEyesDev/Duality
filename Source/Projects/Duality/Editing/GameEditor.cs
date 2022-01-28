using System;
using System.Collections.Generic;
using System.Linq;
using Duality.Editing.Utilities;
using Duality.Editing.Windows;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.ImGui.Standard;

namespace Duality.Editing
{
    public class GameEditor
        : IDisposable
    {
        public GameDriver Driver { get; }
        public ImGUIRenderer Renderer { get; }
        public Dictionary<string, List<TexturePointerMapping>> TextureIcons { get; }
        public bool IsMouseCaptured => _windows.Values.Any(entry => entry.IsMouseHovering);

        public T GetEditingWindow<T>() where T : EditingWindow
        {
            var found = _windows.TryGetValue(typeof(T), out var window);
            return found ? (T) window : null;
        }

        public Texture2D GetSelectedTileTexture()
        {
            var mapping = TextureRegistryWindow.CurrentlySelected;
            if (!mapping.IsValid)
                return null;

            return Driver.TextureRegistry.FindTexture(mapping.TextureType, mapping.TextureName);
        }

        public void Dispose()
        {
            foreach (var (_, mappingList) in TextureIcons)
            {
                foreach (var mapping in mappingList)
                    Renderer.UnbindTexture(mapping.Pointer);
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
            TextureIcons = LoadTextureIcons.Perform(driver, Renderer);
            AddWindow<MouseDataWindow>();
            AddWindow<WorldEditingWindow>();
            AddWindow<TextureRegistryWindow>();
        }

        private void AddWindow<T>() where T : EditingWindow, new()
        {
            var window = new T();
            _windows.Add(typeof(T), window);
        }

        private readonly Dictionary<Type, EditingWindow> _windows = new();
    }

    public readonly struct TexturePointerMapping
    {
        public static TexturePointerMapping Invalid => new("", "", IntPtr.Zero);
        public readonly string TextureType;
        public readonly string TextureName;
        public readonly IntPtr Pointer;
        public bool IsValid => Pointer != IntPtr.Zero;

        public TexturePointerMapping(string textureType, string textureName, IntPtr pointer)
        {
            TextureType = textureType;
            TextureName = textureName;
            Pointer = pointer;
        }
    }
}
