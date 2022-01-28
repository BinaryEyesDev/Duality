using Microsoft.Xna.Framework;

namespace Duality.Components
{
    public static class FrameTime
    {
        public static GameTime Span { get; private set; }
        public static float Scale = 1.0f;
        public static float Elapsed => (float) Span.ElapsedGameTime.TotalSeconds;
        public static float ScaledElapsed => Elapsed*Scale;
        public static void Update(GameTime time) => Span = time;
    }
}
