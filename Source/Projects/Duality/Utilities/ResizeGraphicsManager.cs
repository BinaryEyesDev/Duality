using Microsoft.Xna.Framework;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class ResizeGraphicsManager
    {
        public static void Perform(GraphicsDeviceManager graphics)
        {
            Log.Message("ResizingGraphicsDevice");
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
        }
    }
}
