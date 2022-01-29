using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duality.Components
{
    public class Sprite
    {
        public Transform2D Transform = Transform2D.Identity;
        public bool IsEnabled;
        public bool IsDeleted;

        public string Type;
        public Texture2D Image;
        public Vector2 Size;
        public Vector2 Pivot;
        public Rectangle? Frame;
        public Color Tint;
        public SpriteEffects Effects;
        public float ZIndex;
        public event EventHandler OnEnabled;
        public event EventHandler OnDisabled;
        public Vector2 Origin => Size*Pivot;

        public void Rotate90()
        {
            Transform.Rotation += 90.0f;
            if (Transform.Rotation > 360.0f)
                Transform.Rotation -= 360.0f;
        }

        public void FlipHorizontal()
        {
            var contains = (Effects & SpriteEffects.FlipHorizontally) != 0;
            Effects = contains ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }

        public void Enable(bool isEnabled)
        {
            if (IsEnabled == isEnabled)
                return;

            IsEnabled = isEnabled;
            (IsEnabled ? OnEnabled : OnDisabled)?.Invoke(this, new EventArgs());
        }
    }
}
