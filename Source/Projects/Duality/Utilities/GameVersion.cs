namespace Duality.Utilities
{
    public static class GameVersion
    {
        public const int Major = 1;
        public const int Minor = 0;
        public const int Build = 0;
        public const string Branch = "dev";
        public static string Get() => $"{Major}.{Minor}.{Build}-{Branch}";
    }
}
