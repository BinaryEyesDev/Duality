using System;
using Duality.Components;
using Duality.Extensions;
using Duality.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Data
{
    public class GameWorld
    {
        public string Name;
        public event EventHandler<TileEventArgs> OnTiledPlaced;
        public event EventHandler<TileEventArgs> OnTiledRemoved;
        public GameNode this[GridIndex index] => _nodes[index.Column, index.Row];
        public GameNode GetNode(GridIndex cell) => this[cell];
        public Sprite GetCellSprite(GridIndex cell, int layerId) => this[cell].GetSprite(layerId);

        public int GetWaterTileCount()
        {
            var count = 0;
            RunOnAllTiles.Perform((index, node) =>
            {
                if (node.Layers[0] != null && node.Layers[0].Type.Contains("Water"))
                    count += 1;
            });

            return count;
        }

        public void RemoveSpriteFromNode(GridIndex cell, int layerId)
        {
            Log.Message($"RemovingSpriteFromNode: cell={cell}, layer={layerId}");
            this[cell].RemoveSprite(layerId);

            var worldPosition = CalculateGridFromWorld.GetWorldPosition(cell);
            OnTiledRemoved?.Invoke(this, new TileEventArgs("", cell, worldPosition, layerId));
        }

        public void UpdateSpriteOnNode(GridIndex cell, Texture2D texture, int layerId, string type = "")
        {
            Log.Message($"GeneratingGameNodeSprite: cell={cell}, layer={layerId}");
            var pos = cell.ToXnaVector2()*GlobalConfiguration.TileSize;
            this[cell].AddSprite(texture, layerId, pos, type);

            var worldPosition = CalculateGridFromWorld.GetWorldPosition(cell);
            OnTiledPlaced?.Invoke(this, new TileEventArgs(type, cell, worldPosition, layerId));
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
