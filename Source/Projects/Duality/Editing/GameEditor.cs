using System;
using System.Collections.Generic;
using Duality.Editing.Utilities;
using Duality.Editing.Windows;
using Microsoft.Xna.Framework;
using MonoGame.ImGui.Standard;

namespace Duality.Editing
{
    public class GameEditor
        : IDisposable
    {
        public GameDriver Driver { get; }
        public ImGUIRenderer Renderer { get; }

        public void Dispose()
        {
            foreach (var entry in _iconTextureMap)
                Renderer.UnbindTexture(entry.Pointer);
        }

        public void Update(GameDriver driver, GameTime time)
        {
            
        }

        public void Draw(GameTime time)
        {
            Renderer.BeginLayout(time);
            DrawTopMenu.Perform(this);
            MouseDataWindow.Draw(this);
            WorldEditingWindow.Draw(this);
            TextureRegistryWindow.Draw(this);
            
            Renderer.EndLayout();
        }

        public GameEditor(GameDriver driver)
        {
            Driver = driver;
            Renderer = new ImGUIRenderer(driver)
                .Initialize()
                .RebuildFontAtlas();

            foreach (var (typeName, textureList) in driver.TextureRegistry.Map)
            {
                foreach (var entry in textureList)
                {
                    var mapping = new TexturePointerMapping(typeName, entry.Name, Renderer.BindTexture(entry));
                    _iconTextureMap.Add(mapping);
                }
            }
        }

        private readonly List<TexturePointerMapping> _iconTextureMap = new();
    }

    public readonly struct TexturePointerMapping
    {
        public readonly string ObjectType;
        public readonly string TextureName;
        public readonly IntPtr Pointer;

        public TexturePointerMapping(string objectType, string textureName, IntPtr pointer)
        {
            ObjectType = objectType;
            TextureName = textureName;
            Pointer = pointer;
        }
    }
}
