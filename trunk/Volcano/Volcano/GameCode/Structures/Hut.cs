using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Volcano
{
    class Hut : Strucure
    {
        private static float SPAWN_AREA_X = 2;
        private static float SPAWN_AREA_Y = 2;

        private Stage theStage;

        /// <summary>
        /// Make a new hut.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="center">The center of the hut.</param>
        public Hut(MainGame game, Stage stage, Vector2 center, double width, double height)
            : base(game, center, width, height) { theStage = stage; }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
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
