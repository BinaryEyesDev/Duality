using Duality.Extensions;
using Duality.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Components
{
    public class GameUI
    {
        public float Progress;

        public void Update()
        {
            Progress += FrameTime.ScaledElapsed*0.03f;
            if (Progress > 1.0f)
                Progress = 1.0f;
        }

        public void Draw()
        {
            var viewport = GameDriver.Instance.GraphicsDevice.Viewport;
            _renderer.Begin();

            var pos = new Vector2(5.0f, 25.0f);

            _scoreBarFill.Transform.Position = pos;
            _scoreBarFill.Transform.Scale = new Vector2(Progress, 0.75f);
            _renderer.DrawSprite(_scoreBarFill);

            _scoreBarFrame.Transform.Position = pos;
            _renderer.DrawSprite(_scoreBarFrame);

            _renderer.End();
        }

        public GameUI()
        {
            Log.Message("GeneratingGameUI");
            _renderer = new SpriteBatch(GameDriver.Instance.GraphicsDevice);
            _scoreBarFrame = GenerateSprite.Perform("Textures/UIFrame", false);
            _scoreBarFrame.Pivot = new Vector2(0.0f, 0.0f);
            _scoreBarFrame.Transform.Scale = new Vector2(1.0f, 0.75f);
            _scoreBarFrame.Tint = Color.Gray;

            _scoreBarFill = GenerateSprite.Perform("Textures/UIFill", false);
            _scoreBarFill.Pivot = new Vector2(0.0f, 0.0f);
            _scoreBarFill.Tint = Color.DarkRed*0.8f;
            _scoreBarFill.Transform.Scale = new Vector2(0.0f, 0.75f);

        }

        private readonly Sprite _scoreBarFrame;
        private readonly Sprite _scoreBarFill;
        private readonly SpriteBatch _renderer;
    }
}
