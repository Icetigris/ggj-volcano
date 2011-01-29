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
    /// position. They also have a model and a matrix. Designed to be very simplistic.
    /// </summary>
    class Character : Microsoft.Xna.Framework.IUpdateable, Microsoft.Xna.Framework.IDrawable
    {
        #region variables

        public ContentManager TheContent { get; protected set; }

        public Vector3 Position { get; protected set; }
        public bool IsAlive { get; protected set; }
        public int Health { get; protected set; }

        public Model TheModel { get; protected set; }
        public Matrix TheMatrix { get; protected set; }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public Character(Game mainGame,Vector3 pos, int health)
        {
            TheContent = new ContentManager(mainGame.Services, "Content");

            Position = pos;
            Health = health;
            IsAlive = true;
        }



        #region IUpdateable Members

        public bool Enabled
        {
            get { return true; }
        }

        public event EventHandler EnabledChanged;

        public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public int UpdateOrder
        {
            get { return 0; }
        }

        public event EventHandler UpdateOrderChanged;

        #endregion

        #region IDrawable Members

        public void Draw(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public int DrawOrder
        {
            get { return 0; }
        }

        public event EventHandler DrawOrderChanged;

        public bool Visible
        {
            get { return true; }
        }

        public event EventHandler VisibleChanged;

        #endregion
    }
}
