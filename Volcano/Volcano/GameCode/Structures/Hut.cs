using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
<<<<<<< .mine
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
=======
using Microsoft.Xna.Framework.Graphics;
>>>>>>> .r48

namespace Volcano
{
    class Hut : Strucure
    {
        private static float SPAWN_AREA_X = 2;
        private static float SPAWN_AREA_Y = 2;
        private static float SPAWN_DELAY = 4;
        private static int ENEMY_SPAWN_HP = 1;

        public Stage TheStage { get; private set; }
        public Model TheModel { get; private set; }
        
        private bool loadedContent;
        private float timeTillSpawn;



        private CustomEffects visualEffect;

        /// <summary>
        /// Make a new hut.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="center">The center of the hut.</param>
        public Hut(MainGame game, Stage stage, Vector2 center, double width, double height)
<<<<<<< .mine
            : base(game, center, width, height) { TheStage = stage; }
=======
            : base(game, center, width, height)
        {
            theStage = stage;
            loadedContent = false;
            timeTillSpawn = SPAWN_DELAY;
        }
>>>>>>> .r48

        public override void Draw(GameTime gameTime)
        {
<<<<<<< .mine
            Draw_CustomEffect(gameTime);
=======
            //Game structure needs huts to work, just fuck drawing them

            /*if (!loadedContent)
            {
                loadedContent = true;
                LoadContent();
            }
            //define draw code here.
            float myZoom = 0.0f;

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

                    effect.View = theStage.TheCamera.View;
                    effect.Projection = projection;
                    effect.World = TheRotation * transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(new Vector3(center.X, 100, center.Y));
                }
                mesh.Draw();
            }*/
>>>>>>> .r48
        }





        private void Draw_BasicEffect(GameTime gameTime)
        {
            //define draw code here.
            float myZoom = 5000.0f;

            Matrix[] transforms = new Matrix[TheModel.Bones.Count];
            float aspectRatio = TheGraphics.GraphicsDevice.Viewport.Width / TheStage.GraphicsDevice.Viewport.Height;
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

                    Matrix TheRotation = Matrix.Identity;
                    effect.View = TheStage.TheCamera.View;
                    effect.Projection = projection;
                    effect.World = TheRotation * transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(new Vector3(center.X,10.0f,center.Y));
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

                    Matrix TheRotation = Matrix.CreateRotationX(1.57079633f);
                    Matrix world = transforms[mesh.ParentBone.Index] *
                        Matrix.CreateTranslation(new Vector3(center.X, 10.0f, center.Y));

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
                    effect.Parameters["gIsTiki"].SetValue(true);

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






        public override void Update(GameTime gameTime)
        {
            timeTillSpawn -= (float)gameTime.ElapsedRealTime.TotalSeconds;
            if (timeTillSpawn <= 0)
            {
                timeTillSpawn = SPAWN_DELAY;
                //spawn a tiki
                theStage.enemies.Add(new Enemy(TheGame, theStage, new Vector3(center.X, 0, center.Y), ENEMY_SPAWN_HP));
            }
        }

        protected override void LoadContent()
        {
            TheModel = TheContent.Load<Model>(@"Models\longhouse");
        }

        public Vector2 FindSpawnPosition(GameTime seed)
        {
            Random rand = new Random(seed.TotalRealTime.Milliseconds);
            Vector2 spawnPoint = new Vector2();
            for (int i = 0; i < 10; i++)
            {
                double x = rand.NextDouble() * SPAWN_AREA_X;
                double y = rand.NextDouble() * SPAWN_AREA_Y;
                spawnPoint = center + new Vector2((float)x, (float)y);
                Line lavaTest = new Line(spawnPoint, Vector2.Zero);
                bool doesHit = false;
                foreach(Attack a in TheStage.attacks)
                    if ((a.GetType() == typeof(Aa)) && (a.curHitArea.doesLineHitPoly(lavaTest)))
                        doesHit = true;
                if (!doesHit)
                    break;
            }

            return spawnPoint;
        }
    }
}
