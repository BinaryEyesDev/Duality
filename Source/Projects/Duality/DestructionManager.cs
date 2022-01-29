using Duality.Components;
using Duality.Extensions;
using Duality.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality
{
    public static class DestructionManager
    {
        public static bool InProcess;

        public static void Start()
        {
            if (InProcess) return;
            
            Log.Message("Starting Destruction");
            InProcess = true;
            _flashTimeLeft = 0.1f;
        }

        public static void Update()
        {
            if (!InProcess) return;
            
            _flashTimeLeft -= FrameTime.Elapsed;
            if (_flashTimeLeft > 0.0f) return;

            InProcess = false;
            foreach (var agent in GameDriver.Instance.Agents)
            {
                agent.State = AgentState.Dead;
                agent.Sprite.IsDeleted = true;
            }

            RunOnAllTiles.Perform((index, node) =>
            {
                for (var i = 0; i < node.Layers.Length; i++)
                {
                    if (node.Layers[i] == null) continue;
                    node.Layers[i].IsDeleted = true;
                    node.Layers[i] = null;
                }
            });
        }

        public static void Draw()
        {
            if (!InProcess) return;
         
            _renderer.Begin();
            _renderer.DrawSprite(_screenPanel);
            _renderer.End();
        }

        public static void Initialize()
        {
            Log.Message("InitializingDestructionManager");
            _renderer = new SpriteBatch(GameDriver.Instance.GraphicsDevice);
            _screenPanel = GenerateSprite.Perform("Textures/White", false);
            _screenPanel.Transform.Scale = Vector2.One*1000.0f;
        }

        private static float _flashTimeLeft;
        private static Sprite _screenPanel;
        private static SpriteBatch _renderer;
    }
}
