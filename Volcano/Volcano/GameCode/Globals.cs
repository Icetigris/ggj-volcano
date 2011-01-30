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
        public static Dictionary<Model, Model> convertedModels = new Dictionary<Model, Model>();
        
        public static int numLights = 0;
        public static int maxLights = 4;
        public static Lights[] lights;
    }
}
