using Duality.Components;
using Duality.Extensions;
using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Data
{
    public class GameWorld
    {
        public string Name;

        public GameNode GetNode(GridIndex cell)
        {
            return _nodes[cell.Column, cell.Row];
        }

        public Sprite GetCellSprite(GridIndex cell, int layerId)
        {
            return _nodes[cell.Column, cell.Row].GetSprite(layerId);
        }

        public void RemoveSpriteFromNode(GridIndex cell, int layerId)
        {
            Log.Message($"RemovingSpriteFromNode: cell={cell}, layer={layerId}");
            _nodes[cell.Column, cell.Row].RemoveSprite(layerId);
        }

        public void UpdateSpriteOnNode(GridIndex cell, Texture2D texture, int layerId, string type = "")
        {
            Log.Message($"GeneratingGameNodeSprite: cell={cell}, layer={layerId}");
            var pos = cell.ToXnaVector2()*GlobalConfiguration.TileSize;
            _nodes[cell.Column, cell.Row].AddSprite(texture, layerId, pos, type);
        }

        public GameWorld()
        {
            var mapSize = GlobalConfiguration.MapSize.ToGridIndex();
            _nodes = new GameNode[mapSize.Column, mapSize.Row];
            for (var row = 0; row < mapSize.Row; row++)
            {
                for (var col = 0; col < mapSize.Column; col++)
                {
                    _nodes[col, row] = new GameNode();
                }
            }
        }

        private readonly GameNode[,] _nodes;
    }
}
