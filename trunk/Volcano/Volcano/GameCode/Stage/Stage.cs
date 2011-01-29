using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Volcano
{
    class Stage : DrawableGameComponent
    {
        public Character main { get; set; }
        public List<Character> enemies { get; private set; }
        //TODO: Add map/state/thing?

        public Stage(Game game) : base(game)
        {
            this.enemies = new List<Character>();
        }

        public override void Draw(GameTime gameTime)
        {
            //TODO: Draw the map and volcano
        }
    }
}
