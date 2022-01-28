using Duality.Components;
using Duality.Editing.Windows;
using Microsoft.Xna.Framework;

namespace Duality.Systems
{
    public class UpdateCameraZoom
        : GameSystem
    {
        public float NextZoom;
        
        public override void Perform(GameDriver driver)
        {
            var camera = driver.MainCamera;

            NextZoom = TileEditingWindow.CameraZoomFactor;
            camera.ZoomFactor = MathHelper.Lerp(camera.ZoomFactor, NextZoom, FrameTime.Elapsed*2.0f);
        }
    }
}
