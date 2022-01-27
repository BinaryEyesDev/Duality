using Orca.Logging;

namespace Duality.Editing.Utilities
{
    public static class GenerateGameEditor
    {
        public static GameEditor Perform(GameDriver driver)
        {
            Log.Message("GeneratingGameEditor");
            return new GameEditor(driver);
        }
    }
}
