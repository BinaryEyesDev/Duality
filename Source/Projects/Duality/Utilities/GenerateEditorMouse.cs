using Duality.Components;
using Microsoft.Xna.Framework;

namespace Duality.Utilities
{
    public static class GenerateEditorMouse
    {
        public static EditorMouse Perform()
        {
            var sprite = GenerateSprite.Perform("Textures/SelectionFrame");
            sprite.Tint = Color.Green;
            sprite.ZIndex = 1.0f;

            return new EditorMouse {Sprite = sprite};
        }
    }
}
