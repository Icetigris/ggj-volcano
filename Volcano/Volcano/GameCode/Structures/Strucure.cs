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
        /// Hit area of building.
        /// </summary>
        public Polygon hitArea { get; private set; }

        /// <summary>
        /// The center of the building.
        /// </summary>
        public Vector2 center { get; private set; }
        
        /// <summary>
        /// Make a new structure.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="HitArea">The hit area for the building.</param>
        public Strucure(MainGame game, Vector2 center, double width, double height)
            : base(game)
        {
            this.center = center;
            Vector2 a, b, c, d;
            a = center + new Vector2((float)width / 2, (float)height / 2);
            b = center + new Vector2((float)-width / 2, (float)height / 2);
            c = center + new Vector2((float)-width / 2, (float)-height / 2);
            d = center + new Vector2((float)width / 2, (float)-height / 2);

            hitArea = new Polygon();
            hitArea.Lines.Add(new Line(a, b));
            hitArea.Lines.Add(new Line(b, c));
            hitArea.Lines.Add(new Line(c, d));
            hitArea.Lines.Add(new Line(d, a));
        }

        public abstract override void Draw(GameTime gameTime);

        public abstract override void Update(GameTime gameTime);
    }
}
