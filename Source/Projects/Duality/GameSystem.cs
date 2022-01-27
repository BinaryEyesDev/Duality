using Microsoft.Xna.Framework;

namespace Duality
{
    public abstract class GameSystem
    {
        public abstract void Perform(GameDriver driver, GameTime time);
    }
}
