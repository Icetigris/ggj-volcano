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
    class Enemy : Character
    {
        #region Variables
        /// <summary>
        /// The stage.
        /// </summary>
        Stage theStage;
        #endregion

        public Enemy(MainGame mainGame,Stage stage,Vector3 pos, int health)
            : base(mainGame,stage,pos, health)
        {
            theStage = stage;
            Initialize();
            LoadContent();
        }

        public override void Initialize()
        {
            //nothing to do atm...
        }
        protected override void LoadContent()
        {
            //load model.
            TheModel = TheContent.Load<Model>(@"Models\rocktiki");
            //add model to global list of models.
        }
        public override void UnloadContent()
        {
            TheContent.Unload();
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

        public override void Update(GameTime gameTime)
        {
            //TODO move the enemies (YES THIS MEANS PATHFINDING :\)
            //Berfore/after moving...
            //foreach (Attack a in theStage.attacks)
            //{
                //Check the enemies hitbox with the attack area, if there's a hit do damage.
            //}
            
            //Since we're not really doing anything here...
            base.Update(gameTime);
        }
    }
}
