using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateSpriteBatch
    {
        public static SpriteBatch Perform(GameDriver driver)
        {
            Log.Message("GeneratingSpriteBatch");
            return new SpriteBatch(driver.GraphicsDevice);
        }
    }
}
