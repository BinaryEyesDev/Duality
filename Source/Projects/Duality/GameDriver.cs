using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Duality
{
    /// <summary>
    /// Responsible for stepping through the game's logic and rendering loops.
    /// </summary>
    public class GameDriver
        : Game
    {
        public IGraphicsDeviceManager GraphicsDeviceManager;

        protected override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
