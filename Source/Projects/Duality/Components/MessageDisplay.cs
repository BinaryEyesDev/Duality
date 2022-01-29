using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Components
{
    public class MessageDisplay
    {
        public void AddMessage(string text) => AddMessageToQueue(text, Color.White);
        public void AddDireMessage(string text) => AddMessageToQueue(text, Color.Red);

        public void AddMessageToQueue(string text, Color color)
        {
            if (_messageQueue.TryPeek(out var queuedMessage))
            {
                if (queuedMessage.Text == text) 
                    return;
            }

            var viewport = _driver.GraphicsDevice.Viewport;
            var message = new Message(text)
            {
                Tint = color,
                Position = new Vector2(10.0f, viewport.Height - 20.0f)
            };

            _messageQueue.Enqueue(message);
        }

        public void Update()
        {
            var elapsed = FrameTime.ScaledElapsed;
            TryGenerateMessage(elapsed);

            var completed = new List<Message>();
            var velocity = -Vector2.UnitY*elapsed*20.0f;
            foreach (var message in _activeMessages)
            {
                message.Position += velocity;
                var tint = message.Tint.ToVector4();
                tint *= 1.0f - elapsed*0.25f;
                message.Tint = new Color(tint);

                if (tint.W < 0.01f)
                    completed.Add(message);
            }

            foreach (var entry in completed)
                _activeMessages.Remove(entry);
        }

        private void TryGenerateMessage(float elapsed)
        {
            var viewport = _driver.GraphicsDevice.Viewport;
            var minimumHeight = viewport.Height - 50.0f;

            if (_activeMessages.Count > 0 && _activeMessages[^1].Position.Y > minimumHeight)
                return;

            if (_messageQueue.Count == 0)
                return;

            _activeMessages.Add(_messageQueue.Dequeue());
        }

        public void Draw()
        {
            _renderer.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            var font = _driver.DefaultFont;
            foreach (var message in _activeMessages)
                _renderer.DrawString(font, message.Text, message.Position, message.Tint, 0.0f, Vector2.Zero, Vector2.One*1.15f, SpriteEffects.None, 0.5f);

            _renderer.End();
        }

        public MessageDisplay(GameDriver driver)
        {
            Log.Message("GeneratingMessageDisplay");
            _driver = driver;
            _renderer = new SpriteBatch(driver.GraphicsDevice);
        }

        private readonly GameDriver _driver;
        private readonly SpriteBatch _renderer;
        private readonly List<Message> _activeMessages = new();
        private readonly Queue<Message> _messageQueue = new();
    }

    public class Message
    {
        public readonly string Text;
        public Vector2 Position;
        public Color Tint;

        public Message(string text)
        {
            Text = text;
            Tint = Color.White;
        }
    }
}
