﻿using System;
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
        public static Dictionary<Model, Model> convertedModels = new Dictionary<Model, Model>();
        
        public static int numLights = 0;
        public static int maxLights = 4;
        public static Lights[] lights;

        public static Vector2 PointOnRadius(float radius, float theta)
        {
            return new Vector2(radius * (float) Math.Cos(theta), radius * (float) Math.Sin(theta));
        }

        public static Vector2 RandomPointBetweenRadii(float inner, float outer)
        {
            Random rand = new Random();
            float rad = (float) rand.Next((int) inner, (int) outer);
            float the = (float) rand.NextDouble() * MathHelper.TwoPi;
            return Globals.PointOnRadius(rad, the);
        }
    }
}
