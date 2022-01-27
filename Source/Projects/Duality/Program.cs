using Duality.Utilities;
using Microsoft.Xna.Framework;
using Orca.Logging;
using Orca.Logging.Windows;

namespace Duality
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Register(new ConsoleLog());
            Log.Register(new FileLog("game.log"));
            Log.Message($"Duality v{GameVersion.Get()}");
            Log.Message("Developed By: Amir & Skye Barak");

            using var driver = new GameDriver
            {
                IsMouseVisible = true,
                BackgroundColor = Color.CornflowerBlue,
                Content = {RootDirectory = "Content"},
            };

            driver.Graphics = GenerateGraphicsManager.Perform(driver);
            driver.SpriteBatch = GenerateSpriteBatch.Perform(driver);
            driver.Effect = GenerateBasicEffect.Perform(driver);
            driver.Run();

            Log.Message("ShuttingDown");
            Log.Dispose();
        }
    }
}
