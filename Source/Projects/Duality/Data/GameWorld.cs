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
        public event EventHandler<TileEventArgs> OnLayerObjectAdded;
        public event EventHandler<TileEventArgs> OnLayerObjectRemoved;
        public GameNode this[GridIndex index] => _nodes[index.Column, index.Row];
        public GameNode GetNode(GridIndex cell) => this[cell];
        public Sprite GetCellSprite(GridIndex cell, int layerId) => this[cell].GetSprite(layerId);

        public int GetWaterTileCount()
        {
            var count = 0;
            RunOnAllTiles.Perform((index, node) =>
            {
                if (node.Layers[0] != null && node.Layers[0].Id.SubGroup == "Water")
                    count += 1;
            });

            return count;
        }

        public void RemoveSpriteFromNode(GridIndex cell, int layerId)
        {
            Log.Message($"RemovingSpriteFromNode: cell={cell}, layer={layerId}");
            var id = this[cell].RemoveSprite(layerId);
            if (id.IsInvalid)
                return;

            OnLayerObjectRemoved?.Invoke(this, new TileEventArgs(id, cell));
        }

        public void UpdateSpriteOnNode(GridIndex cell, Texture2D texture, int layerId, GameObjectId? id = null)
        {
            Log.Message($"GeneratingGameNodeSprite: cell={cell}, layer={layerId}");
            var objectId = id ?? GameObjectId.Invalid;
            var pos = cell.ToXnaVector2()*GlobalConfiguration.TileSize;
            this[cell].AddSprite(texture, layerId, pos, objectId);

            OnLayerObjectAdded?.Invoke(this, new TileEventArgs(objectId, cell));
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
