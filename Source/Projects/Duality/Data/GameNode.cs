using Duality.Components;
using Duality.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duality.Data
{
    public class GameNode
    {
        public Sprite[] Layers;

        public Sprite GetSprite(int layerId)
        {
            return ValidateLayerId(layerId) ? Layers[layerId - 1] : null;
        }

        public void RemoveSprite(int layerId)
        {
            if (!ValidateLayerId(layerId))
                return;
            
            var layerIndex = layerId - 1;
            if (Layers[layerIndex] == null) return;

            Layers[layerIndex].IsDeleted = true;
            Layers[layerIndex] = null;
        }

        public bool AddSprite(Texture2D texture, int layerId, Vector2 pos, string type)
        {
            if (!ValidateLayerId(layerId)) 
                return false;

            var layerIndex = layerId - 1;
            if (Layers[layerIndex] == null)
                Layers[layerIndex] = GenerateSprite.Perform(texture);

            Layers[layerIndex].Type = type;
            Layers[layerIndex].Image = texture;
            Layers[layerIndex].ZIndex = GlobalConfiguration.GetZIndexElements(layerId);
            Layers[layerIndex].Transform.Position = pos;

            return true;
        }

        private bool ValidateLayerId(int id)
        {
            return id is >= 1 and < GlobalConfiguration.LayerCount;
        }

        public GameNode()
        {
            Layers = new Sprite[GlobalConfiguration.LayerCount];
        }
    }
}
