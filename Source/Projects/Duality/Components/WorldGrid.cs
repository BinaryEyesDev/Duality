using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duality.Components
{
    public class WorldGrid
    {
        public void Draw(GameDriver driver)
        {
            var spriteBatch = driver.SpriteBatch;

            var start = -_tileSize*0.5f;
            for (var i = 0; i < 64; i++)
            {
                var position = start;
                position.X += _tileSize.X*i;
                spriteBatch.Draw(_image, position, null, _tint, MathHelper.ToRadians(180.0f), _origin, _scale, SpriteEffects.None, 0.0f);
            }

            for (var i = 0; i < 64; i++)
            {
                var position = start;
                position.Y += _tileSize.Y*i;
                spriteBatch.Draw(_image, position, null, _tint, MathHelper.ToRadians(90.0f), _origin, _scale, SpriteEffects.None, 0.0f);
            }
        }

        public WorldGrid(GameDriver driver)
        {
            _tileSize = new Vector2(64.0f, 64.0f);

            _image = new Texture2D(driver.GraphicsDevice, 1, 1);
            _image.SetData(new[]{Color.White});

            _imageSize = new Vector2(_image.Width, _image.Height);
            _scale = new Vector2(1.0f, _tileSize.Y*64);
            _pivot = new Vector2(0.5f, 1.0f);
            _origin = _imageSize*_pivot;
            _tint = Color.White;
        }

        private readonly Vector2 _tileSize;
        private readonly Color _tint;
        private readonly Vector2 _origin;
        private readonly Vector2 _pivot;
        private readonly Vector2 _scale;
        private readonly Vector2 _imageSize;
        private readonly Texture2D _image;
    }
}
