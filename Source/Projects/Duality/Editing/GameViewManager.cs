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

        public static void SetZoomFactory(float value)
        {
            CameraZoomFactor = GlobalConfiguration.LockCameraZoom(value);
        }

        public static int GetLayerId(string subGroupType)
        {
            CurrentTileLayer = FindLayerId(subGroupType);
            return CurrentTileLayer;
        }

        private static int FindLayerId(string subGroupType)
        {
            switch (subGroupType)
            {
                case "Water": return 1;
                case "Grass": return 2;
                case "Nature": return 3;
                case "Buildings": return 4;
                case "Humans": return 5;
                default: return CurrentTileLayer;
            }
        }
    }
}
