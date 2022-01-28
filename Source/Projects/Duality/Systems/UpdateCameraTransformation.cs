using Duality.Extensions;
using Microsoft.Xna.Framework;

namespace Duality.Systems
{
    public class UpdateCameraTransformation
        : GameSystem
    {
        public override void Perform(GameDriver driver)
        {
            var camera = driver.MainCamera;
            var transform = camera.Transform;
            var viewport = camera.Viewport;
            var zoomFactor = camera.ZoomFactor;

            var viewportSize = new Vector3(viewport.Width, viewport.Height, 0.0f);

            var position = -transform.Position.ToXnaVector3();
            var translation = Matrix.CreateTranslation(position);
            var rotation = Matrix.CreateRotationZ(transform.Rotation.ToRadians());
            var scale = Matrix.CreateScale(zoomFactor, zoomFactor, 1.0f);
            var viewTranslation = Matrix.CreateTranslation(viewportSize*0.5f);

            camera.Transformation = translation*rotation*scale*viewTranslation;
            camera.Inverted = Matrix.Invert(camera.Transformation);
        }
    }
}
