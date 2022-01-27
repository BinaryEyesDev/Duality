﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Duality.Systems
{
    public class MovePlayerWithKeyboard
        : GameSystem
    {
        public override void Perform(GameDriver driver, GameTime time)
        {
            var player = driver.Player;
            var elapsed = (float) time.ElapsedGameTime.TotalSeconds;
            var keyboard = Keyboard.GetState();
            var velocity = GetMovementDirection(keyboard)*elapsed*player.MovementSpeed;
            player.Transform.Position += velocity;
        }

        private Vector2 GetMovementDirection(KeyboardState keyboard)
        {
            return new Vector2(
                GetDirection(keyboard, Keys.Right, Keys.Left),
                GetDirection(keyboard, Keys.Down, Keys.Up));
        }

        private float GetDirection(KeyboardState keyboard, Keys pos, Keys neg)
        {
            var direction = 0.0f;
            direction += keyboard.IsKeyDown(pos) ? +1.0f : 0.0f;
            direction += keyboard.IsKeyDown(neg) ? -1.0f : 0.0f;
            return direction;
        }
    }
}