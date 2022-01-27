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
                return new Sprite
                {
                    Image = texture,
                    Size = texture.GetSize(),
                    Pivot = new Vector2(0.5f, 0.5f),
                    Frame = null,
                    Tint = Color.White,
                    Effects = SpriteEffects.None,
                    ZIndex = 0.5f
                };

            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return null;
            }
        }
    }
}
