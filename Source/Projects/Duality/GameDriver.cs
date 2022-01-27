using Duality.Utilities;
using Microsoft.Xna.Framework;

namespace Duality
{
    public class GameDriver
        : Game
    {
        public GraphicsDeviceManager Graphics;

        protected override void Initialize()
        {
            Graphics.PreferredBackBufferWidth = 1280;
            Graphics.PreferredBackBufferHeight = 720;
            Graphics.ApplyChanges();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public GameDriver()
        {
            
        }
    }
}
