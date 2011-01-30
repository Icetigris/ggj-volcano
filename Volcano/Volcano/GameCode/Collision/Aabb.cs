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
    public struct AabbFittings
    {
        public Vector3 min;
        public Vector3 max;
    }
    public struct PreFittedAabbs
    {
        public struct Quadrants
        {
            public static float distance = 10000;
            public struct Q1
            {
                public static Vector3 min = Vector3.Zero;
                public static Vector3 max = new Vector3(distance, distance, distance);
            }
            public struct Q2
            {
                public static Vector3 min = Vector3.Zero;
                public static Vector3 max = new Vector3(distance, distance, -distance);
            }
            public struct Q3
            {
                public static Vector3 min = Vector3.Zero;
                public static Vector3 max = new Vector3(-distance, distance, distance);
            }
            public struct Q4
            {
                public static Vector3 min = Vector3.Zero;
                public static Vector3 max = new Vector3(-distance, distance, -distance);
            }
        }
        public struct Tiki_1
        {
            public static Vector3 min = new Vector3(-10.0f);
            public static Vector3 max = new Vector3(10.0f);
        }
    }

    public class Aabb
    {
        #region variables

        public Vector3 TheMin { get; private set; }
        public Vector3 TheMax { get; private set; }
        public Vector3 Min { get; private set; }
        public Vector3 Max { get; private set; }

        public bool InCollision { get; set; }

        public Character TheCharacter { get; private set; }

        #endregion

        public Aabb(Character character, Vector3 min, Vector3 max)
        {
            TheCharacter = character;
            Min = TheMin = min;
            Max = TheMax = max;
        }

        public void Update(GameTime gameTime)
        {
            if (TheCharacter != null)
            {
                TheMin = TheCharacter.Position + Min;
                TheMax = TheCharacter.Position + Max;
            }
        }
    }
}
