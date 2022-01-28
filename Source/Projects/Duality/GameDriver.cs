using System.Collections.Generic;
using Duality.Components;
using Duality.Data;
using Duality.Editing;
using Duality.Extensions;
using Duality.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Duality
{
    public class GameDriver
        : Game
    {
        public Color BackgroundColor;
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;
        public SpriteFont DefaultFont;
        public FramerateDisplay FramerateDisplay;
        public List<Sprite> Sprites;

        public Camera2D MainCamera;
        public Player Player;
        public WorldGrid WorldGrid;
        public EditorMouse EditorMouse;
        
        public GameWorld World;
        
        public List<GameSystem> UpdateSystems;
        public GameEditor Editor;
        protected override void Initialize() => ResizeGraphicsManager.Perform(Graphics);

        protected override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
                Exit();
            
            for (var i = 0; i < UpdateSystems.Count; i++)
                UpdateSystems[i].Perform(this, gameTime);

            FramerateDisplay.HandleUpdateComplete(gameTime);
            Editor.Update(this, gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BackgroundColor);
            SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, MainCamera.Transformation);

            for (var i = 0; i < Sprites.Count; i++)
            {
                if (Sprites[i].IsEnabled)
                    SpriteBatch.DrawSprite(Sprites[i]);
            }

            WorldGrid.Draw(this);
            SpriteBatch.End();

            FramerateDisplay.HandleDrawComplete();
            Editor.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
