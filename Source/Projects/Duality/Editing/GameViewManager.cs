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
    }
}
