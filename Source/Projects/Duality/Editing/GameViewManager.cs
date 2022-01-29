using Duality.Data;

namespace Duality.Editing
{
    public static class GameViewManager
    {
        public static int CurrentTileLayer = 1;
        public static float CameraZoomFactor = 1.0f;
        public static bool ShowAllLayers = true;
        public static bool ShowLayerMask = false;
        public static bool ShowGrid = false;
        public static int CalculateLayerIndex(int layerId) => layerId - 1;
        public static int CalculateLayerIndex(string subGroupType) => CalculateLayerIndex(FindLayerId(subGroupType));

        public static void SetZoomFactory(float value)
        {
            CameraZoomFactor = GlobalConfiguration.LockCameraZoom(value);
        }

        public static int GetLayerId(string subGroupType)
        {
            CurrentTileLayer = FindLayerId(subGroupType);
            return CurrentTileLayer;
        }

        public static int FindLayerId(string subGroupType)
        {
            switch (subGroupType)
            {
                case "OceanBottom": return 1;
                case "Water": return 2;
                case "Grass": return 3;
                case "Nature": return 4;
                case "Buildings": return 6;
                case "Humans": return 7;
                default: return CurrentTileLayer;
            }
        }
    }
}
