using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duality.Extensions
{
    public static class TextureExtensions
    {
        public static Vector2 GetSize(this Texture2D image)
        {
            return new Vector2(image.Width, image.Height);
        }
    }
}
