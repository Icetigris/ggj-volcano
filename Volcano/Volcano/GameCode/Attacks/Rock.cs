﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Volcano
{
    public class Rock : Attack
    {
        #region Variables

        Model TheModel;
        Matrix TheRotation = Matrix.Identity;
        GraphicsDeviceManager TheGraphics;

        private CustomEffects visualEffect;

        private Vector3 pos;
        public Vector3 Position { get { return pos; } set { pos = value; x = pos.X;  } }
        Stage TheStage;
        MainGame TheGame;

        float x = 0.0f;
        float y = 0.0f;

        #endregion

        public Rock(MainGame mainGame, Stage stage) : base(mainGame)
        {
            TheGame = mainGame;
            TheGraphics = mainGame.graphics;
            TheStage = stage;
            Position = Vector3.Zero;
            this.LoadContent();
        }

        protected override void LoadContent()
        {

            TheModel = TheGame.Content.Load<Model>(@"Models\rock");


            //create custom effect
            visualEffect = new CustomEffects();
            visualEffect.MondoEffect = TheGame.Content.Load<Effect>(@"Effects\MondoEffect");

            //Convert models using custom effects.
            //CustomEffects.ChangeEffectUsedByModel_Textured(TheStage, TheModel, visualEffect.MondoEffect);
        }

        public override void Update(GameTime gameTime)
        {
            // This should work in theory
            //this.Position = new Vector3(x, (float)(-1.0f * Math.Pow(y, 2.0f) + y + 0.0f), x);
            //x--;
        }

        public override void  Draw(GameTime gameTime)
        {
            //Draw_CustomEffect(gameTime);
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
            if (TheStage.TheGame.gameManager.State == TheStage.TheGame.PlayingState ||
                TheStage.TheGame.gameManager.State == TheStage.TheGame.PausedState)
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
                    Matrix world = transforms[mesh.ParentBone.Index] *
                        Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, Position.Z + 1000));

                    DrawModel_Effect(TheModel, transforms, world, projection, gameTime, "MultipleLights");
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
                    effect.Parameters["gIsRock"].SetValue(true);

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
