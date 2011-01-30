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
    public class CollisionManager
    {
        #region variables

        public Stage TheStage { get; private set; }

        public Aabb TheQ1 { get; private set; }
        public Aabb TheQ2 { get; private set; }
        public Aabb TheQ3 { get; private set; }
        public Aabb TheQ4 { get; private set; }

        public bool IsQ1Active { get; private set; }
        public bool IsQ2Active { get; private set; }
        public bool IsQ3Active { get; private set; }
        public bool IsQ4Active { get; private set; }

        #endregion

        public CollisionManager(Stage stage)
        {
            TheStage = stage;

            TheQ1 = new Aabb(stage.main, PreFittedAabbs.Quadrants.Q1.min,
                PreFittedAabbs.Quadrants.Q1.max);
            TheQ2 = new Aabb(stage.main, PreFittedAabbs.Quadrants.Q2.min,
                PreFittedAabbs.Quadrants.Q2.max);
            TheQ3 = new Aabb(stage.main, PreFittedAabbs.Quadrants.Q3.min,
                PreFittedAabbs.Quadrants.Q3.max);
            TheQ4 = new Aabb(stage.main, PreFittedAabbs.Quadrants.Q4.min,
                PreFittedAabbs.Quadrants.Q4.max);

            IsQ1Active = false;
            IsQ2Active = false;
            IsQ3Active = false;
            IsQ4Active = false;
        }

        public void Update(GameTime gameTime)
        {
            //Find if the quadrant is active...
            IsQ1Active = TheStage.main.TheAa.DrawQ1;
            IsQ2Active = TheStage.main.TheAa.DrawQ2;
            IsQ3Active = TheStage.main.TheAa.DrawQ3;
            IsQ4Active = TheStage.main.TheAa.DrawQ4;
        }

        public void ManageCollisions()
        {
            //go through stage's list of enemies, and single player.
            //check if the collsion regions are active.
            //if active, find what enemies are in them.
            //Mark these enemies as hit by the player, as well as
                //what attack hit them.

            if (IsQ1Active)
            {
                for (int c = 0; c < TheStage.enemies.Count; c++)
                {
                    Enemy e = (Enemy)TheStage.enemies[c];
                    e.TheAabb.InCollision = FindAreAabbsCollided(TheQ1, e.TheAabb);

                    //just to be safe...
                    TheStage.enemies[c] = e;
                }
            }
            else if (IsQ2Active)
            {
                for (int c = 0; c < TheStage.enemies.Count; c++)
                {
                    Enemy e = (Enemy)TheStage.enemies[c];
                    e.TheAabb.InCollision = FindAreAabbsCollided(TheQ2, e.TheAabb);

                    //just to be safe...
                    TheStage.enemies[c] = e;
                }
            }
            else if (IsQ3Active)
            {
                for (int c = 0; c < TheStage.enemies.Count; c++)
                {
                    Enemy e = (Enemy)TheStage.enemies[c];
                    e.TheAabb.InCollision = FindAreAabbsCollided(TheQ3, e.TheAabb);

                    //just to be safe...
                    TheStage.enemies[c] = e;
                }
            }
            else if (IsQ4Active)
            {
                for (int c = 0; c < TheStage.enemies.Count; c++)
                {
                    Enemy e = (Enemy)TheStage.enemies[c];
                    e.TheAabb.InCollision = FindAreAabbsCollided(TheQ4, e.TheAabb);

                    //just to be safe...
                    TheStage.enemies[c] = e;
                }
            }
        }

        //Check and see if the enemy is in collision with the
        //active region.
        public bool FindAreAabbsCollided(Aabb quadrant, Aabb enemy)
        {
            //if (CheckAreVec3Equal(enemy.TheMin, quadrant.TheMin) > 0 &&
            //    CheckAreVec3Equal(enemy.TheMax, quadrant.TheMax) < 0)
            //    return true;

            //return false;

            return AreNumbersOrdered(quadrant.TheMin.X, enemy.TheMin.X, quadrant.TheMax.X) &&
                    AreNumbersOrdered(quadrant.TheMin.X, enemy.TheMax.X, quadrant.TheMax.X) &&
                    AreNumbersOrdered(quadrant.TheMin.Y, enemy.TheMin.Y, quadrant.TheMax.Y) &&
                    AreNumbersOrdered(quadrant.TheMin.Y, enemy.TheMax.Y, quadrant.TheMax.Y) &&
                    AreNumbersOrdered(quadrant.TheMin.Z, enemy.TheMin.Z, quadrant.TheMax.Z) &&
                    AreNumbersOrdered(quadrant.TheMin.Z, enemy.TheMax.Z, quadrant.TheMax.Z);
        }

        private bool AreNumbersOrdered(float a, float b, float c)
        {
            return (a<=b && b<=c) || (c<=b && b<=a);
        }
        /// <summary>
        /// returns (-1) if all components of first less than second.
        /// returns (0) if all components of first equal to second.
        /// returns (+1) if all components of first greater than second.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        //private int CheckAreVec3Equal(Vector3 first, Vector3 second)
        //{
        //    if (first.X == second.X &&
        //        first.Y == second.Y &&
        //        first.Z == second.Z)
        //        return 0;

        //    else if (first.X <= second.X &&
        //        first.Y <= second.Y &&
        //        first.Z <= second.Z)
        //        return -1;

        //    else if (first.X >= second.X &&
        //        first.Y >= second.Y &&
        //        first.Z >= second.Z)
        //        return 1;


        //    return 0;
        //}

    }
}
