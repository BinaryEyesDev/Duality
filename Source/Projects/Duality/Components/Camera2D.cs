using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duality.Components
{
    public class Camera2D
    {
        public Transform2D Transform = Transform2D.Identity;
        public float FollowSpeed;
        public Viewport Viewport;
        public Matrix Transformation;
        public Matrix Inverted;

        public float ZoomFactor
        {
            get => Transform.Scale.X;
            set => Transform.Scale.X = value;
        }
    }
}
