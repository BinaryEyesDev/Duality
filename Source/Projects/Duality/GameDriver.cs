using System.Collections.Generic;
using System.Security.Cryptography;
using Duality.Components;
using Duality.Data;
using Duality.Editing;
using Duality.Extensions;
using Duality.Registries;
using Duality.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Orca.Logging;

namespace Duality
{
    public class GameDriver
        : Game
    {
        public static GameDriver Instance { get; private set; }
        public static bool Exists => Instance != null;

        public Color BackgroundColor;
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;
        public SpriteFont DefaultFont;
        public FramerateDisplay FramerateDisplay;
        public MessageDisplay MessageDisplay;
        public GameUI UI;

        public GameWorld World;
        public TextureRegistry TextureRegistry;
        public CreatureRegistry CreatureRegistry;
        
        public List<Sprite> Sprites;
        public List<Track> TrackingComponents;
        public List<IAgent> Agents;

        public Camera2D MainCamera;
        public LayerDarkMask LayerMasker;
        public Player Player;
        public WorldGrid WorldGrid;
        public EditorMouse EditorMouse;

        public List<GameSystem> UpdateSystems;
        public GameEditor Editor;
        public GameDriver() => Instance = this;
        protected override void Initialize() => ResizeGraphicsManager.Perform(Graphics);

        protected override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
                Exit();

            MouseInput.Update();
            FrameTime.Update(gameTime);

            if (!DestructionManager.InProcess)
            {
                for (var i = 0; i < UpdateSystems.Count; i++)
                    UpdateSystems[i].Perform(this);

                for (var i = Sprites.Count - 1; i >= 0; i--)
                {
                    if (!Sprites[i].IsDeleted) continue;
                    Sprites.RemoveAt(i);
                }

                for (var i = Agents.Count - 1; i >= 0; i--)
                {
                    if (Agents[i].State != AgentState.Dead) continue;
                    Agents.RemoveAt(i);
                }

                MessageDisplay.Update();
                UI.Update();
                Editor.Update(this, gameTime);
            }
            else
                DestructionManager.Update();

            FramerateDisplay.HandleUpdateComplete(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BackgroundColor);
            SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, MainCamera.Transformation);

            for (var i = 0; i < Sprites.Count; i++)
            {
                if (Sprites[i].IsEnabled)
                    SpriteBatch.DrawSprite(Sprites[i]);
            }

            WorldGrid.Draw(this);
            SpriteBatch.End();

            MessageDisplay.Draw();
            FramerateDisplay.HandleDrawComplete();
            UI.Draw();
            Editor.Draw(gameTime);

            if (DestructionManager.InProcess)
                DestructionManager.Draw();

            base.Draw(gameTime);
        }
    }
}
