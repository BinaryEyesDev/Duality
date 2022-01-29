using Duality.Components;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GeneratePlayer
    {
        public static Player Perform()
        {
            Log.Message("GeneratingPlayer");
            var sprite = GenerateSprite.Perform("Textures/MockPlayer");
            sprite.Transform.Position = GameDriver.Instance.MainCamera.Transform.Position;
            sprite.IsEnabled = false;
            sprite.ZIndex = 0.4f;
            return new Player
            {
                MovementSpeed = 250.0f,
                Sprite = sprite
            };
        }
    }
}
