using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Volcano
{
    /// <summary>
    /// Abstract class representing an attack.
    /// </summary>
    public abstract class Attack : DrawableGameComponent
    {
        public Character TheCharacter { get; private set; }

        /// <summary>
        /// Creates a new Attack.
        /// </summary>
        /// <param name="game">The game.  (You just lost.)</param>
        public Attack(Game game) : base(game) { }

        /// <summary>
        /// The damage caused by this attack.
        /// </summary>
        public int damage { get; protected set; }

        /// <summary>
        /// The current hit area for the attack.
        /// </summary>
        public Polygon curHitArea { get; protected set; }

        /// <summary>
        /// Updates the attack - should change the curHitArea.
        /// </summary>
        /// <param name="gameTime">Time that has occured since the last update.</param>
        public abstract override void Update(GameTime gameTime);

        /// <summary>
        /// Draws the current attack.
        /// </summary>
        /// <param name="gameTime">Time that has occured since the last draw.</param>
        public abstract override void Draw(GameTime gameTime);
    }
}
