using Duality.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Duality.Components
{
    public static class MouseInput
    {
        public static Vector2 GetMovementDelta() => _delta;
        public static Vector2 GetScreenPosition() => _position;
        public static bool IsButtonUp(int buttonIndex) => _current.IsButtonPressed(buttonIndex);
        public static bool IsButtonDown(int buttonIndex) => _current.IsButtonPressed(buttonIndex);

        public static bool WasButtonJustReleased(int buttonIndex)
        {
            return _previous.IsButtonPressed(buttonIndex) && _current.IsButtonReleased(buttonIndex);
        }

        public static bool WasButtonJustPressed(int buttonIndex)
        {
            return _previous.IsButtonReleased(buttonIndex) && _current.IsButtonPressed(buttonIndex);
        }

        public static void Update()
        {
            _previous = _current;
            _current = Mouse.GetState();

            _position = _current.Position.ToVector2();
            _delta = _position - _previous.Position.ToVector2();
        }

        private static Vector2 _delta;
        private static Vector2 _position;
        private static MouseState _current;
        private static MouseState _previous;
    }
}
