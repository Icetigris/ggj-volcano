using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Volcano
{
    class Hut : Strucure
    {
        /// <summary>
        /// Make a new hut.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="HitArea">The hit area for the building (doubles as the position).</param>
        public Hut(MainGame game, Polygon HitArea) : base(game, HitArea) { }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
