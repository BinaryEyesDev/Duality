using Duality.Components;
using Microsoft.Xna.Framework;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateLayerMasker
    {
        public static LayerDarkMask Perform(GameDriver driver)
        {
            Log.Message("GeneratingLayerMasker");
            var sprite = GenerateSprite.Perform(driver, "Textures/Masker");
            sprite.Transform.Scale = Vector2.One*1000.0f;

            return new LayerDarkMask
            {
                Sprite = sprite
            };
        }
    }
}
