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
    class Player : Character, Microsoft.Xna.Framework.IUpdateable, Microsoft.Xna.Framework.IDrawable
    {
        #region Variables
        #endregion

        public Player(Game mainGame,Vector3 pos, int health)
            : base(mainGame,pos, health)
        {
            Initialize();
            LoadContent();
        }

        private void Initialize()
        {
            //nothing to do atm...
        }
        private void LoadContent()
        {
            //load model.
            TheModel = TheContent.Load<Model>(@"Models\cartridge_multi");
            //add model to global list of models.
        }
        public void UnloadContent()
        {
            TheContent.Unload();
        }

        #region IUpdateable Members

        bool IUpdateable.Enabled
        {
            get { throw new NotImplementedException(); }
        }

        event EventHandler IUpdateable.EnabledChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        void IUpdateable.Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        int IUpdateable.UpdateOrder
        {
            get { throw new NotImplementedException(); }
        }

        event EventHandler IUpdateable.UpdateOrderChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        #endregion

        #region IDrawable Members

        void IDrawable.Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        int IDrawable.DrawOrder
        {
            get { throw new NotImplementedException(); }
        }

        event EventHandler IDrawable.DrawOrderChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        bool IDrawable.Visible
        {
            get { throw new NotImplementedException(); }
        }

        event EventHandler IDrawable.VisibleChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        #endregion
    }
}
