using System;
using Duality.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

            try
            {
                using var driver = new GameDriver
                {
                    IsMouseVisible = true,
                    BackgroundColor = Color.CornflowerBlue,
                    Content = {RootDirectory = "Content"},
                };

                driver.Graphics = GenerateGraphicsManager.Perform(driver);
                driver.SpriteBatch = GenerateSpriteBatch.Perform(driver);
                driver.DefaultFont = GenerateDefaultFont.Perform(driver);
                driver.FramerateDisplay = GenerateFramerateDisplay.Perform(driver);
                driver.MainCamera = GenerateMainCamera.Perform();
                driver.Sprite = GenerateSprite.Perform(driver, "Tiles/WaterTile_5");

                driver.Window.Title = $"Duality {GameVersion.Get()}";
                driver.Run();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Console.ReadKey();
            }

            Log.Message("ShuttingDown");
            Log.Dispose();
        }
    }
}
