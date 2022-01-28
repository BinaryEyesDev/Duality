using Duality.Extensions;

namespace Duality.Data
{
    public class GameWorld
    {
        public string Name;

        public GameWorld()
        {
            var mapSize = GlobalConfiguration.MapSize.ToGridIndex();
            _nodes = new GameNode[mapSize.Column, mapSize.Row];
        }

        private GameNode[,] _nodes;
    }
}
