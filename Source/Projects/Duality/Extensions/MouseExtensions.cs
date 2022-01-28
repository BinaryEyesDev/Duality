using System;
using Microsoft.Xna.Framework.Input;

namespace Duality.Extensions
{
    public static class MouseExtensions
    {
        public static bool IsButtonReleased(this MouseState state, int buttonId)
        {
            return state.GetMouseButton(buttonId) == ButtonState.Released;
        }

        public static bool IsButtonPressed(this MouseState state, int buttonId)
        {
            return state.GetMouseButton(buttonId) == ButtonState.Pressed;
        }

        public static ButtonState GetMouseButton(this MouseState state, int buttonId)
        {
            switch (buttonId)
            {
                case 0: return state.LeftButton;
                case 1: return state.MiddleButton;
                case 2: return state.RightButton;
                default: throw new NullReferenceException($"mouse button not mapped to given id: {buttonId}");
            }
        }
    }
}
