using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Volcano
{
    public abstract class Strucure : DrawableGameComponent
    {
        /// <summary>
        /// Health of building.
        /// </summary>
        public int hp { get; private set; }

        /// <summary>
        /// Hit area of building (doubles as the position).
        /// </summary>
        public Polygon hitArea { get; private set; }
        
        /// <summary>
        /// Make a new structure.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="HitArea">The hit area for the building (doubles as the position).</param>
        public Strucure(MainGame game, Polygon HitArea) : base(game) { hitArea = HitArea; }

        public abstract override void Draw(GameTime gameTime);

        public abstract override void Update(GameTime gameTime);
    }
}
