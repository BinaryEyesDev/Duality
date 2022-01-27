using Duality.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duality.Components
{
    public class Camera2D
    {
        public Transform2D Transform = Transform2D.Identity;
        public Matrix Transformation;
        public float ZoomFactor => Transform.Scale.X;

        public void Update(Viewport viewport)
        {
            var viewportSize = new Vector3(viewport.Width, viewport.Height, 0.0f);

            var position = -Transform.Position.ToVector3();
            var translation = Matrix.CreateTranslation(position);
            var rotation = Matrix.CreateRotationZ(Transform.Rotation.ToRadians());
            var scale = Matrix.CreateScale(ZoomFactor, ZoomFactor, 1.0f);
            var viewTranslation = Matrix.CreateTranslation(viewportSize * 0.5f);

            Transformation = translation*rotation*scale*viewTranslation;
        }
    }
}
