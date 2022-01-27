using Microsoft.Xna.Framework;

namespace Duality.Systems
{
    public class CameraTrackTarget
        : GameSystem
    {
        public override void Perform(GameDriver driver, GameTime time)
        {
            var elapsed = (float) time.ElapsedGameTime.TotalSeconds;
            var player = driver.Player;
            var camera = driver.MainCamera;

            var next = Vector2.Lerp(camera.Transform.Position, player.Transform.Position, elapsed*camera.FollowSpeed);
            camera.Transform.Position = next;
        }
    }
}
