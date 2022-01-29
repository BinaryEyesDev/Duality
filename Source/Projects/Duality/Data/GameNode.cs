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

        public GameObjectId RemoveSprite(int layerId)
        {
            if (!ValidateLayerId(layerId))
                return GameObjectId.Invalid;
            
            var layerIndex = layerId - 1;
            if (Layers[layerIndex] == null) 
                return GameObjectId.Invalid;

            var id = Layers[layerIndex].Id;
            Layers[layerIndex].IsDeleted = true;
            Layers[layerIndex] = null;
            return id;
        }

        public bool AddSprite(Texture2D texture, int layerId, Vector2 pos, GameObjectId id)
        {
            if (!ValidateLayerId(layerId)) 
                return false;

            var layerIndex = layerId - 1;
            if (Layers[layerIndex] == null)
                Layers[layerIndex] = GenerateSprite.Perform(texture);

            Layers[layerIndex].Id = id;
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
