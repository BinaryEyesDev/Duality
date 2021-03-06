using System.Collections.Generic;
using Duality.Systems;
using Orca.Logging;

namespace Duality.Utilities
{
    public static class GenerateUpdateSystems
    {
        public static List<GameSystem> Perform()
        {
            Log.Message("GeneratingUpdateSystems");
            return new List<GameSystem>
            {
                new UpdateDyingAgents(),
                new ShowLayers(),
                new UpdateLayerMasker(),
                new MovePlayerWithKeyboard(),
                new PerformAgentAI(),
                new CameraTrackTarget(),
                new UpdateCameraZoom(),
                new UpdateCameraTransformation(),
                new RaycastMouse(),
            };
        }
    }
}
