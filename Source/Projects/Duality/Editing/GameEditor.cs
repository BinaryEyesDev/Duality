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
        public Dictionary<string, List<TexturePointerMapping>> TextureIcons { get; }

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

            TextureIcons = new Dictionary<string, List<TexturePointerMapping>>();
            foreach (var (typeName, textureList) in driver.TextureRegistry.Map)
            {
                if (!TextureIcons.ContainsKey(typeName))
                    TextureIcons.Add(typeName, new List<TexturePointerMapping>());

                foreach (var entry in textureList)
                {
                    var mapping = new TexturePointerMapping(entry.Name, Renderer.BindTexture(entry));
                    TextureIcons[typeName].Add(mapping);
                }
            }
        }
    }

    public readonly struct TexturePointerMapping
    {
        public readonly string TextureName;
        public readonly IntPtr Pointer;

        public TexturePointerMapping(string textureName, IntPtr pointer)
        {
            TextureName = textureName;
            Pointer = pointer;
        }
    }
}
