using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using XELibrary;

namespace Volcano
{
    public class Player : Character
    {
        #region Variables
        /// <summary>        
        /// The stage.        
        /// </summary>
        private Stage theStage;

        public InputHandler TheInput { get; private set; }

        public int Pressure { get; private set; }
        public int MaxPressure { get; private set; }
        public int MinPressure { get; private set; }
        public int InitialPressure { get; private set; }

        #endregion

        /// <summary>
        /// Constructor for the Player.        
        /// </summary>        
        /// <param name="mainGame">The game.</param>        
        /// <param name="s">The stage.</param>        
        /// <param name="pos">Position to spawn the player at.</param>        
        /// <param name="health">Health to give the player.</param>        
        public Player(MainGame mainGame,Stage stage,Vector3 pos, int health)
            : base(mainGame,stage,pos, health)
        {
            Pressure = 0;
            InitialPressure = Pressure;

            MaxPressure = 100;
            MinPressure = 0;

            theStage = stage;
            Initialize(mainGame);
            LoadContent();
        }

        private void Initialize(MainGame mainGame)
        {
            //get our input manager...
            TheInput = mainGame.input;
        }
        protected override void LoadContent()
        {
            //load model.
            TheModel = TheContent.Load<Model>(@"Models\volcano");
            //add model to global list of models.
        }
        public override void UnloadContent()
        {
            TheContent.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            int offset = 5;

            //increase/decrease player pressure. Testing purporses.
            if (TheInput.KeyboardState.IsKeyDown(Keys.I) &&
                (Pressure + offset) <= MaxPressure)
                    Pressure += offset;
            if (TheInput.KeyboardState.IsKeyDown(Keys.O) &&
                    (Pressure - offset) >= 0)
                    Pressure -= offset;

        }

        public override void Draw(GameTime gameTime)
        {
            //define draw code here.
            float myZoom = 5000.0f;

            Matrix[] transforms = new Matrix[TheModel.Bones.Count];
            float aspectRatio = TheGraphics.GraphicsDevice.Viewport.Width / TheGraphics.GraphicsDevice.Viewport.Height;
            TheModel.CopyAbsoluteBoneTransformsTo(transforms);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                aspectRatio, 1.0f, 10000.0f);
            Matrix view = Matrix.CreateLookAt(new Vector3(0.0f, 50.0f, myZoom), Vector3.Zero, Vector3.Up);



            foreach (ModelMesh mesh in TheModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();

                    //effect.View = view;
                    //effect.Projection = projection;
                    //effect.World = TheRotation * transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(Position);

                    effect.View = TheStage.TheCamera.View;
                    effect.Projection = projection;
                    effect.World = TheRotation * transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(Position);
                }
                mesh.Draw();
            }
        }
    }
}
