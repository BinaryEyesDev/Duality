using Duality.Components;
using Duality.Extensions;
using Duality.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Duality
{
    public class GameDriver
        : Game
    {
        public Color BackgroundColor;
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;
        public BasicEffect Effect;
        public Sprite Sprite;

        protected override void Initialize()
        {
            ResizeGraphicsManager.Perform(Graphics);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BackgroundColor);
            SpriteBatch.Begin(
                SpriteSortMode.Deferred, 
                BlendState.AlphaBlend,
                SamplerState.AnisotropicClamp,
                DepthStencilState.Default,
                RasterizerState.CullCounterClockwise,
                null, 
                Matrix.Identity);
                

            SpriteBatch.DrawSprite(Sprite);

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
