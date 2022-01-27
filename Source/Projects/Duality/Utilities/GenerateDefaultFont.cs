using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateDefaultFont
    {
        public static SpriteFont Perform(GameDriver driver)
        {
            Log.Message("GeneratingDefaultFont");
            return driver.Content.Load<SpriteFont>("Fonts/default");
        }
    }
}
