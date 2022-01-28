using System;
using Duality.Components;
using Duality.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateSprite
    {
        public static Sprite Perform(GameDriver driver, string texturePath)
        {
            try
            {
                var texture = driver.Content.Load<Texture2D>(texturePath);
                return Perform(driver, texture);
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return null;
            }
        }

        public static Sprite Perform(GameDriver driver, Texture2D texture)
        {
            Log.Message($"GeneratingSprite: texture={texture.Name}");
            var sprite = new Sprite
            {
                IsEnabled = true,
                Image = texture,
                Size = texture.GetSize(),
                Pivot = new Vector2(0.5f, 0.5f),
                Frame = null,
                Tint = Color.White,
                Effects = SpriteEffects.None,
                ZIndex = 0.5f,
            };

            driver.Sprites.Add(sprite);
            return sprite;
        }
    }
}
