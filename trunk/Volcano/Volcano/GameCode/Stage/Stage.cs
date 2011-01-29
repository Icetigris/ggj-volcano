using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Volcano
{
    public class Stage : DrawableGameComponent
    {
        /// <summary>
        /// The main character.
        /// </summary>
        public Character main { get; set; }

        /// <summary>
        /// The enemies.  (No need to ever set this, since List is mutable.)
        /// </summary>
        public List<Character> enemies { get; private set; }

        public PlayerCamera TheCamera { get; private set; }

        /// <summary>
        /// The active attacks.  (No need to ever set this, since List is mutable.)
        /// </summary>
        public List<Attack> attacks { get; private set; }

        /// <summary>
        /// The game.  (You just lost.)
        /// </summary>
        public MainGame TheGame { get; private set; }
        //TODO: Add map/state/thing?

        public Stage(MainGame game) : base(game)
        {
            TheGame = game;
            this.enemies = new List<Character>();

            Initialize();
        }

        private void Initialize()
        {
            base.Initialize();

            
        }

        public void LoadContent()
        {
            //create player
            main = new Player(TheGame, this, Vector3.Zero, 100);
            TheCamera = new PlayerCamera(TheGame);
        }

        public void Unload()
        {
            main.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            //need to update camera
            TheCamera.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //TODO: Draw the map and volcano

            //draw player
            main.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //Oprah: AND YOU GET AN UPDATE!
            main.Update(gameTime);
            
            //AND YOU ALL GET UPDATES!
            foreach(Enemy e in enemies)
                e.Update(gameTime);

            //EVERYBODY GETS UPDATES!!!
            foreach (Attack a in attacks)
                a.Update(gameTime);
        }
    }
}
