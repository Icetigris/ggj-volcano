using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

using XELibrary;

namespace Volcano
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PlayerCamera : Microsoft.Xna.Framework.GameComponent, Microsoft.Xna.Framework.IGameComponent
    {
        protected MainGame TheGame;
        protected IInputHandler input;

        private Matrix projection;
        private Matrix view;

        protected Vector3 cameraPosition = new Vector3(0.0f, 600.0f, 3.0f);
        private Vector3 cameraTarget = Vector3.Zero;
        private Vector3 cameraUpVector = Vector3.Up;

        private Vector3 cameraReference = new Vector3(0.0f, 0.0f, -1.0f);

        private const float spinRate = 120.0f;
        private const float moveRate = 120.0f;
        private float theta;

        protected Vector3 movement = Vector3.Zero;

        protected int playerIndex = 0;

        private Viewport? viewport;

        public PlayerCamera(MainGame game)
            : base(game)
        {
            TheGame = game;
            input = game.input;
            theta = 0.0f;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
            InitializeCamera();
        }


        private void InitializeCamera()
        {
            //Projection
            float aspectRatio = (float)Game.GraphicsDevice.Viewport.Width /
                (float)Game.GraphicsDevice.Viewport.Height;
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio,
                1.0f, 10000.0f, out projection);

            //View
            Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget,
                ref cameraUpVector, out view);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            if (TheGame.gameManager.State == TheGame.PlayingState)
            {
                float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
                float radius = 5000.0f;

                if (input.KeyboardState.IsKeyDown(Keys.Left) ||
                    (input.GamePads[playerIndex].IsButtonDown(Buttons.RightThumbstickLeft)) ||
                    (input.GamePads[playerIndex].IsButtonDown(Buttons.DPadLeft)))
                {
                    theta += timeDelta;
                }
                if (input.KeyboardState.IsKeyDown(Keys.Right) ||
                    (input.GamePads[playerIndex].IsButtonDown(Buttons.RightThumbstickRight)) ||
                    (input.GamePads[playerIndex].IsButtonDown(Buttons.DPadRight)))
                {
                    theta -= timeDelta;
                }

                if (input.KeyboardState.IsKeyDown(Keys.Down) ||
                    (input.GamePads[playerIndex].IsButtonDown(Buttons.RightThumbstickDown)) ||
                    (input.GamePads[playerIndex].IsButtonDown(Buttons.DPadDown)))
                {
                    if (cameraPosition.Y >= 400.0f) cameraPosition.Y -= 200;
                }
                if (input.KeyboardState.IsKeyDown(Keys.Up) ||
                    (input.GamePads[playerIndex].IsButtonDown(Buttons.RightThumbstickUp)) ||
                    (input.GamePads[playerIndex].IsButtonDown(Buttons.DPadUp)))
                {
                    if (cameraPosition.Y <= 7000.0f) cameraPosition.Y += 200;
                }

                cameraPosition.X = radius * (float)Math.Cos(theta);
                cameraPosition.Z = radius * (float)Math.Sin(theta);

                Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget, ref cameraUpVector,
                    out view);
            }

            base.Update(gameTime);
        }

        public Matrix View
        {
            get { return view; }
        }

        public Matrix Projection
        {
            get { return projection; }
        }

        public PlayerIndex PlayerIndex
        {
            get { return ((PlayerIndex)playerIndex); }
            set { playerIndex = (int)value; }
        }

        public Vector3 Position
        {
            get { return (cameraPosition); }
            set { cameraPosition = value; }
        }

        public Vector3 Orientation
        {
            get { return (cameraReference); }
            set { cameraReference = value; }
        }

        public Vector3 Target
        {
            get { return (cameraTarget); }
            set { cameraTarget = value; }
        }
        public Viewport Viewport
        {
            get
            {
                if (viewport == null)
                    viewport = Game.GraphicsDevice.Viewport;

                return ((Viewport)viewport);
            }
            set
            {
                viewport = value;
                InitializeCamera();
            }
        }
    }
}