using System;
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
        }

        public override void Update(GameTime gameTime)
        {
            // This should work in theory
            //this.Position = new Vector3(x, (float)(-1.0f * Math.Pow(y, 2.0f) + y + 0.0f), x);
            //x--;
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
