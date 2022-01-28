using Duality.Components;

namespace Duality.Utilities
{
    public static class GenerateEditorMouse
    {
        public static EditorMouse Perform(GameDriver driver)
        {
            var sprite = GenerateSprite.Perform(driver, "Textures/WhiteFrame");
            sprite.ZIndex = 0.0f;

            return new EditorMouse {Sprite = sprite};
        }
    }
}
