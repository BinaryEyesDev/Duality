using Duality.Utilities;

namespace Duality
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using var driver = new GameDriver();
            driver.Graphics = GenerateGraphicsManager.Perform(driver);
            driver.Run();
        }
    }
}
