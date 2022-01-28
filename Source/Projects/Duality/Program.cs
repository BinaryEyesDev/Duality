using System;
using System.Collections.Generic;
using Duality.Components;
using Duality.Editing.Utilities;
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
                driver.MainCamera = GenerateMainCamera.Perform(driver);
                driver.Sprites = new List<Sprite>();

                driver.Player = GeneratePlayer.Perform(driver);
                driver.WorldGrid = GenerateWorldGrid.Perform(driver);
                driver.EditorMouse = GenerateEditorMouse.Perform(driver);

                driver.World = GenerateGameWorld.Perform(driver, "DefaultWorld");
                driver.UpdateSystems = GenerateUpdateSystems.Perform();
                driver.Window.Title = $"Duality {GameVersion.Get()}";

                driver.Editor = GenerateGameEditor.Perform(driver);
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
