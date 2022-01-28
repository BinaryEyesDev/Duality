using System;
using Duality.Components;
using Duality.Editing.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Duality.Systems
{
    public class UpdateCameraZoom
        : GameSystem
    {
        public float NextZoom;
        
        public override void Perform(GameDriver driver)
        {
            var camera = driver.MainCamera;

            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.LeftAlt))
            {
                var scrollDelta = MouseInput.GetScrollDelta();
                TileEditingWindow.SetZoomFactory(TileEditingWindow.CameraZoomFactor + scrollDelta*0.001f);
                    ;
            }

            NextZoom = TileEditingWindow.CameraZoomFactor;
            camera.ZoomFactor = MathHelper.Lerp(camera.ZoomFactor, NextZoom, FrameTime.Elapsed*2.0f);
        }
    }
}
