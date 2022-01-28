using Duality.Components;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GeneratePlayer
    {
        public static Player Perform(GameDriver driver)
        {
            Log.Message("GeneratingPlayer");
            var sprite = GenerateSprite.Perform(driver, "Textures/MockPlayer");
            sprite.IsEnabled = false;
            sprite.ZIndex = 0.4f;
            return new Player
            {
                MovementSpeed = 100.0f,
                Sprite = sprite
            };
        }
    }
}
