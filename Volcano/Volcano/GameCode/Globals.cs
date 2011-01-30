using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Volcano
{
    class Globals
    {
        
        public static int numLights = 0;
        public static int maxLights = 4;
        public static Lights[] lights;
        public static Vector3 topOfVolcano = new Vector3(0, 500, 0); //This is a guess.

        public static Vector2 PointOnRadius(float radius, float theta)
        {
            return new Vector2(radius * (float) Math.Cos(theta), radius * (float) Math.Sin(theta));
        }
    }
}
