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
    public class Aa : Attack
    {
        #region Variables

        public Model TheQ1 { get; private set; }
        public Model TheQ2 { get; private set; }
        public Model TheQ3 { get; private set; }
        public Model TheQ4 { get; private set; }

        public Player ThePlayer { get; private set; }

        private CustomEffects visualEffect;

        public bool DrawQ1 { get; private set; }
        public bool DrawQ2 { get; private set; }
        public bool DrawQ3 { get; private set; }
        public bool DrawQ4 { get; private set; }

        public Texture2D TheLavaTex { get; private set; }

        public Timer TheDrawTimer;

        #endregion

        /// <summary>
        /// Creates a new Aa lava flow.
        /// </summary>
        public Aa(Game game, Player player) : base(game) {
            ThePlayer = player;

            DrawQ1 = false;
            DrawQ2 = false;
            DrawQ3 = false;
            DrawQ4 = false;
        }

        public void LoadContent()
        {
            TheQ1 = ThePlayer.TheContent.Load<Model>(@"Models\volcano-q4");
            TheQ2 = ThePlayer.TheContent.Load<Model>(@"Models\volcano-q1");
            TheQ3 = ThePlayer.TheContent.Load<Model>(@"Models\volcano-q3");
            TheQ4 = ThePlayer.TheContent.Load<Model>(@"Models\volcano-q2");

            TheLavaTex = ThePlayer.TheContent.Load<Texture2D>(@"Textures\TempLava");

            //create custom effect
            visualEffect = new CustomEffects();
            visualEffect.MondoEffect = ThePlayer.TheContent.Load<Effect>(@"Effects\MondoEffect");

            //Convert models using custom effects.
            CustomEffects.ChangeEffectUsedByModel(ThePlayer.TheStage,TheQ1, visualEffect.MondoEffect);
            CustomEffects.ChangeEffectUsedByModel(ThePlayer.TheStage,TheQ2, visualEffect.MondoEffect);
            CustomEffects.ChangeEffectUsedByModel(ThePlayer.TheStage,TheQ3, visualEffect.MondoEffect);
            CustomEffects.ChangeEffectUsedByModel(ThePlayer.TheStage,TheQ4, visualEffect.MondoEffect);

            TheDrawTimer = new Timer();
        }

        public override void Update(GameTime gameTime)
        {
            TheDrawTimer.Update(gameTime);

            //TODO: This will update the flow of the lava, changing the current hit polygon.
            if (ThePlayer.TheInput.KeyboardState.IsKeyDown(Keys.Y))
            {
                DrawQ1 = true;
                DrawQ2 = false;
                DrawQ3 = false;
                DrawQ4 = false;
            }
            else if (ThePlayer.TheInput.KeyboardState.IsKeyDown(Keys.H))
            {
                DrawQ1 = false;
                DrawQ2 = true;
                DrawQ3 = false;
                DrawQ4 = false;
            }
            else if (ThePlayer.TheInput.KeyboardState.IsKeyDown(Keys.G))
            {
                DrawQ1 = false;
                DrawQ2 = false;
                DrawQ3 = true;
                DrawQ4 = false;
            }
            else if (ThePlayer.TheInput.KeyboardState.IsKeyDown(Keys.J))
            {
                DrawQ1 = false;
                DrawQ2 = false;
                DrawQ3 = false;
                DrawQ4 = true;
            }

            if (DrawQ1 || DrawQ2 || DrawQ3 || DrawQ4)
            {
                var LengthOfAttack = 0.8f; //OK WHO USED VAR?!
                if (TheDrawTimer.TargetTimeSeconds(LengthOfAttack))
                {
                    DrawQ1 = false;
                    DrawQ2 = false;
                    DrawQ3 = false;
                    DrawQ4 = false;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            //TODO: draws it, derp
            Draw_CustomEffect(gameTime);
        }


        private void Draw_CustomEffect(GameTime gameTime)
        {
            Model TempModel = null;
            if (DrawQ1)
                TempModel = TheQ1;
            else if (DrawQ2)
                TempModel = TheQ2;
            else if (DrawQ3)
                TempModel = TheQ3;
            else if (DrawQ4)
                TempModel = TheQ4;


            if (TempModel != null)
            {

                visualEffect.Init();
                if (ThePlayer.TheStage.TheGame.gameManager.State == ThePlayer.TheStage.TheGame.PlayingState ||
                    ThePlayer.TheStage.TheGame.gameManager.State == ThePlayer.TheStage.TheGame.PausedState)
                {
                    Matrix[] transforms = new Matrix[ThePlayer.TheModel.Bones.Count];
                    ThePlayer.TheModel.CopyAbsoluteBoneTransformsTo(transforms);

                    float aspectRatio = ThePlayer.TheGraphics.GraphicsDevice.Viewport.Width / ThePlayer.TheGraphics.GraphicsDevice.Viewport.Height;
                    Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                        aspectRatio, 1.0f, 1000000.0f);

                    foreach (ModelMesh mesh in TempModel.Meshes)
                    {
                        visualEffect.Set_Phong_Diffuse(new Vector3(1.0f, 1.0f, 1.0f), Vector4.One);
                        visualEffect.Set_Phong_Ambient(Vector4.One, new Vector4(0.1f, 0.1f, 0.1f, 1.0f));
                        visualEffect.Set_Phong_Specular(new Vector4(0.8f, 0.8f, 0.8f, 1.0f), Vector4.One, 20.0f);

                        Matrix TheRotation = Matrix.CreateRotationX(1.57079633f);
                        Matrix world = ThePlayer.TheRotation * transforms[mesh.ParentBone.Index] *
                            Matrix.CreateTranslation(new Vector3(ThePlayer.Position.X, ThePlayer.Position.Y + 250, ThePlayer.Position.Z + 1000));

                        DrawModel_Effect(TempModel, transforms, world, projection, gameTime, "LavaAttack_Light");
                    }
                }
            }
        }


        private void DrawModel_Effect(Model model, Matrix[] transform, Matrix world, Matrix projection, GameTime gameTime, string technique)
        {
            // Set suitable renderstates for drawing a 3D model.
            RenderState renderState = ThePlayer.TheGraphics.GraphicsDevice.RenderState;

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
                    effect.Parameters["gWVP"].SetValue(localWorld * (ThePlayer.TheStage.TheCamera.View) * projection);
                    effect.Parameters["gEyePosW"].SetValue(ThePlayer.TheStage.TheCamera.Position);

                    effect.Parameters["gNumLights"].SetValue(Globals.numLights);
                    effect.Parameters["gTex"].SetValue(TheLavaTex);
                    effect.Parameters["gTime"].SetValue(visualEffect.Update_Time(gameTime));

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
