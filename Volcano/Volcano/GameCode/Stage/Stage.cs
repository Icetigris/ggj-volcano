using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Volcano
{
    public class Stage : DrawableGameComponent
    {
        public Character main { get; set; }
        public List<Character> enemies { get; private set; }
        public Game TheGame { get; private set; }
        //TODO: Add map/state/thing?

        public Stage(Game game) : base(game)
        {
            TheGame = game;
            this.enemies = new List<Character>();

            Initialize();
        }

        private void Initialize()
        {
            //create player
            main = new Player(TheGame, Vector3.Zero, 100);
        }

        public void Unload()
        {
            main.UnloadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            //TODO: Draw the map and volcano

            //draw player
            main.Draw(gameTime);
        }
    }
}
