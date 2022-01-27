using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duality.Components
{
    public class Camera2D
    {
        public Transform2D Transform = Transform2D.Identity;
        public Viewport Viewport;
        public Matrix Transformation;
        public float ZoomFactor => Transform.Scale.X;
    }
}
