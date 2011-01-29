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
    /// <summary>
    /// Characters are living entities that can live and attack. This
    /// includes the player and enemies. Characters have: health, a state of life,
    /// position. Designed to be very simplistic.
    /// </summary>
    class Characrer
    {
        #region variables

        public Vector3 Position { get; private set; }
        public bool IsAlive { get; private set; }
        public int Health { get; private set; }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        //public Character(Vector3 pos, int health)
        //{
        //    Position = pos;
        //    Health = health;
        //    IsAlive = true;
        //}
    }
}
