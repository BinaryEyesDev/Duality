using System;

namespace Duality.Utilities
{
    public static class GetRandom
    {
        public static Type GetCurrentType() => _rng.GetType();
        public static int Int32(int min, int max) => _rng.Next(min, max);
        public static float Float() => (float)_rng.NextDouble();
        public static float Float(float min, float max) => min + (max - min)*Float();
        public static double Double() => _rng.NextDouble();
        public static double Double(double min, double max) => min + (max - min)*Double();
        static GetRandom() => _rng = new Random();
        private static readonly Random _rng;
    }
}
