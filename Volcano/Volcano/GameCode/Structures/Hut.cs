using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Volcano
{
    class Hut : Strucure
    {
        private static float SPAWN_AREA_X = 2;
        private static float SPAWN_AREA_Y = 2;
        private static float SPAWN_DELAY = 4;
        private static int ENEMY_SPAWN_HP = 1;

        private Stage theStage;
        private bool loadedContent;
        private float timeTillSpawn;

        /// <summary>
        /// Make a new hut.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="center">The center of the hut.</param>
        public Hut(MainGame game, Stage stage, Vector2 center, double width, double height)
            : base(game, center, width, height)
        {
            theStage = stage;
            loadedContent = false;
            timeTillSpawn = SPAWN_DELAY;
        }

        public override void Draw(GameTime gameTime)
        {
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
                foreach(Attack a in theStage.attacks)
                    if ((a.GetType() == typeof(Aa)) && (a.curHitArea.doesLineHitPoly(lavaTest)))
                        doesHit = true;
                if (!doesHit)
                    break;
            }

            return spawnPoint;
        }
    }
}
