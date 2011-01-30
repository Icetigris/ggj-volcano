using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Volcano
{
    class Pahoehoe : Attack
    {
        /// <summary>
        /// Creates a new Pahoehoe lava flow.
        /// </summary>
        /// <param name="game"></param>
        /// 
        #region Variables

        float flowRate; //tweak variable for changing lava rate of flow


        #endregion

        public Pahoehoe(Game game) : base(game) { }

        public override void Update(GameTime gameTime)
        {
            //TODO: This will update the flow of the lava, changing the current hit polygon.
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime)
        {
            //TODO: draws it, derp
            throw new NotImplementedException();
        }
    }
}
