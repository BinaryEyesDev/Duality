using Duality.Components;
using Microsoft.Xna.Framework.Graphics;

namespace Duality.Extensions
{
    public static class SpriteExtensions
    {
        public static SpriteBatch DrawSprite(this SpriteBatch batch, Sprite sprite)
        {
            batch.Draw(sprite.Image,
                sprite.Transform.Position,
                sprite.Frame,
                sprite.Tint,
                sprite.Transform.Rotation.ToRadians(),
                sprite.Origin,
                sprite.Transform.Scale,
                sprite.Effects,
                sprite.ZIndex);

            return batch;
        }
    }
}
