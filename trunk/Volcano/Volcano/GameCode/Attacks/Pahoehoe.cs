﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Volcano
{
    class Pahoehoe : Attack
    {
        /// <summary>
        /// Creates a new Pahoehoe.
        /// </summary>
        /// <param name="game">The game.  (You just lost.)</param>
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