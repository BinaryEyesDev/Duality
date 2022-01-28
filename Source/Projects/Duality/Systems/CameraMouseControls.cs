using Duality.Components;

namespace Duality.Systems
{
    public class CameraMouseControls
        : GameSystem
    {
        public override void Perform(GameDriver driver)
        {
            var camera = driver.MainCamera;
            var scroll = MouseInput.GetScrollDelta();

            var nextZoom = camera.ZoomFactor + scroll*FrameTime.Elapsed*0.01f;
            camera.ZoomFactor = nextZoom < 0.1f ? 0.1f : nextZoom > 2.0f ? 2.0f : nextZoom;

        }
    }
}
