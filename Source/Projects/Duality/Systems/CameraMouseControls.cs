using Duality.Components;
using Microsoft.Xna.Framework;

namespace Duality.Systems
{
    public class CameraMouseControls
        : GameSystem
    {
        public float NextZoom;
        
        public override void Perform(GameDriver driver)
        {
            var camera = driver.MainCamera;
            var scroll = MouseInput.GetScrollDelta();

            NextZoom = camera.ZoomFactor + scroll*0.01f;
            NextZoom = NextZoom < 0.1f ? 0.1f : NextZoom > 2.0f ? 2.0f : NextZoom;
            camera.ZoomFactor = MathHelper.Lerp(camera.ZoomFactor, NextZoom, FrameTime.Elapsed*2.0f);
        }
    }
}
