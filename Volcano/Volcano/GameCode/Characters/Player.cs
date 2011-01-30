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


        private CustomEffects visualEffect;

        public InputHandler TheInput { get; private set; }

        public int Pressure { get; private set; }
        public int MaxPressure { get; private set; }
        public int MinPressure { get; private set; }
        public int InitialPressure { get; private set; }

        public Aa TheAa { get; private set; }

        #endregion

        /// <summary>
        /// Constructor for the Player.        
        /// </summary>        
        /// <param name="mainGame">The game.</param>        
        /// <param name="s">The stage.</param>        
        /// <param name="pos">Position to spawn the player at.</param>        
        /// <param name="health">Health to give the player.</param>        
        public Player(MainGame mainGame, Stage stage, Vector3 pos, int health)
            : base(mainGame, stage, pos, health)
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
            TheAa = new Aa(mainGame, this);
        }
        protected override void LoadContent()
        {
            //load model.
            TheModel = TheContent.Load<Model>(@"Models\volcano");

            //create custom effect
            visualEffect = new CustomEffects();
            visualEffect.MondoEffect = TheContent.Load<Effect>(@"Effects\MondoEffect");

            //Convert models using custom effects.
            CustomEffects.ChangeEffectUsedByModel(TheStage,TheModel, visualEffect.MondoEffect);

            TheAa.LoadContent();

        }
        public override void UnloadContent()
        {
            TheContent.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            int offset = 5;

            if (theStage.TheGame.gameManager.State == theStage.TheGame.PlayingState)
            {
                //increase/decrease player pressure. Testing purporses.
                if (TheInput.KeyboardState.IsKeyDown(Keys.I) &&
                    (Pressure + offset) <= MaxPressure)
                    Pressure += offset;
                if (TheInput.KeyboardState.IsKeyDown(Keys.O) &&
                        (Pressure - offset) >= 0)
                    Pressure -= offset;

                TheAa.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            //Draw_BasicEffect(gameTime);

            Draw_CustomEffect(gameTime);
            TheAa.Draw(gameTime);
        }

        private void Draw_BasicEffect(GameTime gameTime)
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

        private void Draw_CustomEffect(GameTime gameTime)
        {

            visualEffect.Init();
            if (theStage.TheGame.gameManager.State == theStage.TheGame.PlayingState ||
                theStage.TheGame.gameManager.State == theStage.TheGame.PausedState)
            {
                Matrix[] transforms = new Matrix[TheModel.Bones.Count];
                TheModel.CopyAbsoluteBoneTransformsTo(transforms);

                float aspectRatio = TheGraphics.GraphicsDevice.Viewport.Width / TheGraphics.GraphicsDevice.Viewport.Height;
                Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                    aspectRatio, 1.0f, 1000000.0f);

                foreach (ModelMesh mesh in TheModel.Meshes)
                {
                    visualEffect.Set_Phong_Diffuse(new Vector3(1.0f, 1.0f, 1.0f), Vector4.One);
                    visualEffect.Set_Phong_Ambient(Vector4.One, new Vector4(0.1f, 0.1f, 0.1f, 1.0f));
                    visualEffect.Set_Phong_Specular(new Vector4(0.8f, 0.8f, 0.8f, 1.0f), Vector4.One, 20.0f);

                    TheRotation = Matrix.CreateRotationX(1.57079633f);
                    Matrix world = TheRotation * transforms[mesh.ParentBone.Index] * 
                        Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, Position.Z + 1000));

                    DrawModel_Effect(TheModel, transforms, world, projection, gameTime,"MultipleLights");
                }
            }
        }


        private void DrawModel_Effect(Model model, Matrix[] transform, Matrix world, Matrix projection, GameTime gameTime, string technique)
        {
            // Set suitable renderstates for drawing a 3D model.
            RenderState renderState = TheGraphics.GraphicsDevice.RenderState;

            renderState.AlphaBlendEnable = false;
            renderState.AlphaTestEnable = false;
            renderState.DepthBufferEnable = true;

            // Look up the bone transform matrices.
            Matrix[] transforms = new Matrix[model.Bones.Count];

            model.CopyAbsoluteBoneTransformsTo(transforms);

            // Draw the model.
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    // Specify which effect technique to use.
                    effect.CurrentTechnique = effect.Techniques[technique];

                    Matrix localWorld = transforms[mesh.ParentBone.Index] * world;

                    effect.Parameters["gWorld"].SetValue(localWorld);
                    effect.Parameters["gWIT"].SetValue(Matrix.Invert(Matrix.Transpose(localWorld)));
                    effect.Parameters["gWInv"].SetValue(Matrix.Invert(localWorld));
                    effect.Parameters["gWVP"].SetValue(localWorld * (TheStage.TheCamera.View) * projection);
                    effect.Parameters["gEyePosW"].SetValue(TheStage.TheCamera.Position);

                    effect.Parameters["gNumLights"].SetValue(Globals.numLights);
                    //effect.Parameters["gTex"].SetValue(the);
                    //effect.Parameters["gTime"].SetValue(visualEffect.Update_Time(gameTime));

                    String parameter;
                    for (int v = 0; v < Globals.numLights; v++)
                    {
                        parameter = "gLightPos_multiple_" + (v + 1);
                        effect.Parameters[parameter].SetValue(Globals.lights[v]._position);
                        parameter = "gDiffuseMtrl_multiple_" + (v + 1);
                        effect.Parameters[parameter].SetValue(Globals.lights[v]._diffuse_material);
                        parameter = "gDiffuseLight_multiple_" + (v + 1);
                        effect.Parameters[parameter].SetValue(Globals.lights[v]._diffuse_light);
                        parameter = "gSpecularMtrl_multiple_" + (v + 1);
                        effect.Parameters[parameter].SetValue(Globals.lights[v]._specular_material);
                        parameter = "gSpecularLight_multiple_" + (v + 1);
                        effect.Parameters[parameter].SetValue(Globals.lights[v]._specular_light);
                    }

                    //effect.Parameters["gLightVecW"].SetValue(new Vector3(0.0f, -1.0f, 0.0f));
                    //effect.Parameters["gDiffuseMtrl"].SetValue(new Vector4(1.0f));
                    //effect.Parameters["gDiffuseLight"].SetValue(Color.White.ToVector4());
                    effect.Parameters["gAmbientMtrl"].SetValue(Color.White.ToVector4());
                    effect.Parameters["gAmbientLight"].SetValue(new Vector4(0.1f));
                    //effect.Parameters["gSpecularMtrl"].SetValue(new Vector4(0.8f, 0.8f, 0.8f, 1.0f));
                    //effect.Parameters["gSpecularLight"].SetValue(Color.White.ToVector4());
                    effect.Parameters["gSpecularPower"].SetValue(20.0f);

                    effect.CommitChanges();
                }

                mesh.Draw();
            }
        }


    }
}