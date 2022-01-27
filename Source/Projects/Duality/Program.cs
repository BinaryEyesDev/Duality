using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;
using Orca.Logging.Windows;

namespace Duality
{
    /// <summary>
    /// Provides the main entry point into the application.
    /// </summary>
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Register(new ConsoleLog());
            Log.Register(new FileLog("duality.log"));
            Log.Message("Duality v1.0.0");
            Log.Message("Developed By: Skye & Amir Barak");

            Log.Message("GeneratingGameDriver");
            using (var driver = new GameDriver())
            {
                driver.BackgroundColor = Color.CornflowerBlue;
                driver.GraphicsDeviceManager = GenerateGraphicsDeviceManager(driver);
                driver.Run();
            }

            Log.Message("ShuttingDown");
            Log.Dispose();
        }

        private static IGraphicsDeviceManager GenerateGraphicsDeviceManager(Game game)
        {
            Log.Message("Generating: GraphicsDeviceManager");
            return new GraphicsDeviceManager(game)
            {
                GraphicsProfile = GraphicsProfile.Reach,
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
                IsFullScreen = false,
                PreferMultiSampling = true,
                SynchronizeWithVerticalRetrace = false
            };
        }
    }
}
