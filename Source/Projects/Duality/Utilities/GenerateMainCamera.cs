using Duality.Components;
using Microsoft.Xna.Framework;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateMainCamera
    {
        public static Camera2D Perform()
        {
            Log.Message("GeneratingMainCamera");
            return new Camera2D {Transformation = Matrix.Identity};
        }
    }
}
