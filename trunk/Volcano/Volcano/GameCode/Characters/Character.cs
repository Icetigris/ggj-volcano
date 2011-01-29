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
    class Characrer
    {
        #region variables

        public Vector3 Position { get; private set; }
        public bool IsAlive { get; private set; }
        public int Health { get; private set; }



        #endregion
    }
}
