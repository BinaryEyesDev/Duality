using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duality.Components
{
    public class FramerateDisplay
    {
        public FramerateDisplay(GameDriver driver)
        {
            _driver = driver;
            _renderer = new SpriteBatch(driver.GraphicsDevice);
        }

        public void HandleUpdateComplete(GameTime time)
        {
            _elapsed += time.ElapsedGameTime;

            if (_elapsed <= TimeSpan.FromSeconds(1.0f))
                return;

            _elapsed -= TimeSpan.FromSeconds(1.0f);
            _framesPerSecond = _counter;
            _secondsPerFrame = 1.0f/_framesPerSecond;
            _counter = 0;
        }

        public void HandleDrawComplete()
        {
            _counter++;
            _lineIndex = 0;
            _renderer.Begin();
            Write(_framesPerSecond);
            Write(_secondsPerFrame);
            _renderer.End();
        }

        private void Write(float value)
        {
            var device = _driver.GraphicsDevice;
            var font = _driver.DefaultFont;

            const float offset = 10.0f;
            var valueString = $"{value}";
            var stringSize = font.MeasureString(valueString);
            var screenWidth = device.Viewport.Width;
            var positionX = screenWidth - stringSize.X - offset;
            var positionY = offset + (stringSize.Y + 3.0f) * _lineIndex;

            var position = new Vector2(positionX, positionY);
            var shadowPosition = position - new Vector2(-1.0f, -1.0f);

            _renderer.DrawString(font, valueString, position, Color.Black);
            _renderer.DrawString(font, valueString, shadowPosition, Color.White);

            _lineIndex += 1;
        }

        private int _counter;
        private TimeSpan _elapsed;

        private int _lineIndex;
        private int _framesPerSecond;
        private float _secondsPerFrame;
        private readonly SpriteBatch _renderer;
        private readonly GameDriver _driver;
    }
}
