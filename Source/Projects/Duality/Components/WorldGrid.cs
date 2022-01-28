using Duality.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duality.Components
{
    public class WorldGrid
    {
        public void Draw(GameDriver driver)
        {
            var camera = driver.MainCamera;
            _batch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.AnisotropicClamp,
                DepthStencilState.Default,
                RasterizerState.CullCounterClockwise,
                null,
                camera.Transformation);

            var start = new Vector2(-1024.0f, 0.0f);
            for (var i = 0; i < 55; i++)
            {
                var step = start.X + 64.0f*i;
                var location = new Vector2(step, 0.0f);
                _batch.Draw(_line, location, null, Color.White, 0.0f, _origin, _scale, SpriteEffects.None, 0.0f);
            }

            for (var i = 0; i < 55; i++)
            {
                var step = start.Y + 64.0f*i;
                var location = new Vector2(0.0f, step);
                _batch.Draw(_line, location, null, Color.White, 90.0f.ToRadians(), _origin, _scale, SpriteEffects.None, 0.0f);
            }

            _batch.End();
        }

        public WorldGrid(GameDriver driver)
        {
            _line = driver.Content.Load<Texture2D>("Textures/White");
            _batch = new SpriteBatch(driver.GraphicsDevice);
        }

        private readonly Vector2 _origin = new Vector2(512.0f, 512.0f)*0.5f;
        private readonly Vector2 _scale = new(1.0f/512.0f, 2.0f);
        private readonly Texture2D _line;
        private readonly SpriteBatch _batch;
    }
}
