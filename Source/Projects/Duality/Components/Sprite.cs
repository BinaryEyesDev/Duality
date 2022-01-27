using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duality.Components
{
    public class Sprite
    {
        public Transform2D Transform = Transform2D.Identity;

        public Texture2D Image;
        public Vector2 Size;
        public Vector2 Pivot;
        public Rectangle? Frame;
        public Color Tint;
        public SpriteEffects Effects;
        public float ZIndex;
        public Vector2 Origin => Size*Pivot;
    }
}
