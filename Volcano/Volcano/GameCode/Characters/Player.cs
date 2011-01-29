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
    class Player : Character
    {
        #region Variables
        #endregion

        public Player(Game mainGame,Vector3 pos, int health)
            : base(mainGame,pos, health)
        {
            Initialize();
            LoadContent();
        }

        private void Initialize()
        {
            //nothing to do atm...
        }
        private void LoadContent()
        {
            //load model.
            //TheModel = TheContent.Load<Model>(@"Models\volcano");
            //add model to global list of models.
        }
        public override void UnloadContent()
        {
            TheContent.Unload();
        }


        public override void Draw(GameTime gameTime)
        {
            //define draw code here.
            float myZoom = 1.0f;

            Matrix[] transforms = new Matrix[TheModel.Bones.Count];
            float aspectRatio = TheGraphics.Viewport.Width / TheGraphics.Viewport.Height;
            TheModel.CopyAbsoluteBoneTransformsTo(transforms);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                aspectRatio, 1.0f, 10000.0f);
            Matrix view = Matrix.CreateLookAt(new Vector3(0.0f, 50.0f, myZoom), Vector3.Zero, Vector3.Up);

            foreach (ModelMesh mesh in TheModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();

                    effect.View = view;
                    effect.Projection = projection;
                    effect.World = TheRotation * transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(Position);
                }
                mesh.Draw();
            }
        }
    }
}
