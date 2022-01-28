using Duality.Components;
using Duality.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duality.Data
{
    public class GameNode
    {
        public const int LayerCount = 5;
        public Sprite[] Layers;

        public Sprite GetSprite(int layerId)
        {
            return layerId is < 1 or > 5 ? null : Layers[layerId - 1];
        }

        public void RemoveSprite(int layerId)
        {
            if (layerId is < 1 or > 5) return;
            
            var layerIndex = layerId - 1;
            if (Layers[layerIndex] == null) return;

            Layers[layerIndex].IsDeleted = true;
            Layers[layerIndex] = null;
        }

        public bool AddSprite(Texture2D texture, int layerId, Vector2 pos)
        {
            if (layerId is < 1 or > 5) return false;

            var layerIndex = layerId - 1;
            if (Layers[layerIndex] == null)
                Layers[layerIndex] = GenerateSprite.Perform(GameDriver.Instance, texture);

            Layers[layerIndex].Image = texture;
            Layers[layerIndex].ZIndex = layerId*GlobalConfiguration.SpriteLayerStep;
            Layers[layerIndex].Transform.Position = pos;

            return true;
        }

        public GameNode()
        {
            Layers = new Sprite[LayerCount];
        }
    }
}
