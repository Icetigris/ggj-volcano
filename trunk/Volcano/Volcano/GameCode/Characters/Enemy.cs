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
    class Enemy : Character
    {
        #region Variables
        #endregion

        public Enemy(Game mainGame,Vector3 pos, int health)
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
        public override void UnloadContent()
        {
            TheContent.Unload();
        }



        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
