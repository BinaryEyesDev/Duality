using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateBasicEffect
    {
        public static BasicEffect Perform(GameDriver driver)
        {
            Log.Message("GeneratingBasicEffect");
            return new BasicEffect(driver.GraphicsDevice);
        }
    }
}
