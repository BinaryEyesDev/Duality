using Duality.Components;
using Microsoft.Xna.Framework;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateMainCamera
    {
        public static Camera2D Perform(GameDriver driver)
        {
            Log.Message("GeneratingMainCamera");
            var camera = new Camera2D
            {
                FollowSpeed = 5.0f,
                Viewport = driver.GraphicsDevice.Viewport,
                Transformation = Matrix.Identity
            };

            driver.GraphicsDevice.DeviceReset += (_, _) => camera.Viewport = driver.GraphicsDevice.Viewport;
            return camera;
        }
    }
}
