using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duality.Utilities
{
    public static class GenerateGraphicsManager
    {
        public static GraphicsDeviceManager Perform(Game game)
        {
            var manager = new GraphicsDeviceManager(game)
            {
                GraphicsProfile = GraphicsProfile.Reach,
                IsFullScreen = false,
                SynchronizeWithVerticalRetrace = false,
                PreferMultiSampling = true
            };

            manager.ApplyChanges();
            return manager;
        }
    }
}
