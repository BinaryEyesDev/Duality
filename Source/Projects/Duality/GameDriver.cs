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
        public SpriteFont DefaultFont;
        public FramerateDisplay FramerateDisplay;
        public Camera2D MainCamera;
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

            MainCamera.Update(GraphicsDevice.Viewport);

            FramerateDisplay.HandleUpdateComplete(gameTime);
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
                MainCamera.Transformation);

            SpriteBatch.DrawSprite(Sprite);

            SpriteBatch.End();

            FramerateDisplay.HandleDrawComplete();
            base.Draw(gameTime);
        }
    }
}
