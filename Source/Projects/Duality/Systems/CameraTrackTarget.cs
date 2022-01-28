using Duality.Components;
using Microsoft.Xna.Framework;

namespace Duality.Systems
{
    public class CameraTrackTarget
        : GameSystem
    {
        public override void Perform(GameDriver driver)
        {
            var player = driver.Player;
            var camera = driver.MainCamera;

            var next = Vector2.Lerp(camera.Transform.Position, player.Transform.Position, FrameTime.Elapsed*camera.FollowSpeed);
            camera.Transform.Position = next;
        }
    }
}
