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
        public MainGame TheGame { get; private set; }
        public PlayerCamera TheCamera { get; private set; }
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
            main = new Player(TheGame,this, Vector3.Zero, 100);
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
    }
}
