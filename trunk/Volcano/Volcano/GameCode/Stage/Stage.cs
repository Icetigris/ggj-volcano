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
        public Player main { get; set; }

        /// <summary>
        /// The enemies.  (No need to ever set this, since List is mutable.)
        /// </summary>
        public List<Character> enemies { get; private set; }

        /// <summary>
        /// The active attacks.  (No need to ever set this, since List is mutable.)
        /// </summary>
        public List<Attack> attacks { get; private set; }

        /// <summary>
        /// The structures.  (No need to ever set this, since List is mutable.)
        /// </summary>
        public List<Strucure> structures { get; private set; }

        /// <summary>
        /// The camera.
        /// </summary>
        public PlayerCamera TheCamera { get; private set; }

        /// <summary>
        /// The game.  (You just lost.)
        /// </summary>
        public MainGame TheGame { get; private set; }
        //TODO: Add map/state/thing?

        public Stage(MainGame game) : base(game)
        {
            TheGame = game;
            this.enemies = new List<Character>();
            this.attacks = new List<Attack>();
            this.structures = new List<Strucure>();

            Initialize();
        }

        private new void Initialize()
        {
            base.Initialize();

            
        }

        public new void LoadContent()
        {
            //create player
            main = new Player(TheGame, this, Vector3.Zero, 100);
            TheCamera = new PlayerCamera(TheGame);
        }

        public void Unload()
        {
            main.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //need to update camera
            TheCamera.Update(gameTime);

            //and the main
            main.Update(gameTime);

            //and all the enemies
            foreach (Enemy e in enemies)
                e.Update(gameTime);

            //and all the attacks
            foreach (Attack a in attacks)
                a.Update(gameTime);

            //and all the structures
            foreach (Strucure s in structures)
                s.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //TODO: Draw the map and volcano
            // - Isn't this just the player?  ~Ben

            //draw player
            main.Draw(gameTime);

            //and all the enemies
            foreach (Enemy e in enemies)
                e.Draw(gameTime);

            //and all the attacks
            foreach (Attack a in attacks)
                a.Draw(gameTime);

            //and all the structures
            foreach (Strucure s in structures)
                s.Draw(gameTime);
        }
    }
}
