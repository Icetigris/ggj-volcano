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
        /// <summary>
        /// The stage.
        /// </summary>
        Stage theStage;
        #endregion

        public Enemy(MainGame mainGame,Stage stage,Vector3 pos, int health)
            : base(mainGame,stage,pos, health)
        {
            theStage = stage;
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

        public override void Update(GameTime gameTime)
        {
            //TODO move the enemies (YES THIS MEANS PATHFINDING :\)
            //Berfore/after moving...
            //foreach (Attack a in theStage.attacks)
            //{
                //Check the enemies hitbox with the attack area, if there's a hit do damage.
            //}
            
            //Since we're not really doing anything here...
            base.Update(gameTime);
        }
    }
}
