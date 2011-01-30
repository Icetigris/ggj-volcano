using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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


        public CollisionManager TheCollisionManager { get; set; }

        /// <summary>
        /// The camera.
        /// </summary>
        public PlayerCamera TheCamera { get; private set; }

        public MainGame TheGame { get; private set; }
        //TODO: Add map/state/thing?

        bool doOnce = true;
        public Texture2D TheBackground { get; private set; }


        public Dictionary<Model, Model> convertedModels { get; set; }

        public Stage(MainGame game) : base(game)
        {
            TheGame = game;
            this.enemies = new List<Character>();
            this.attacks = new List<Attack>();
            this.structures = new List<Strucure>();
            TheCollisionManager = new CollisionManager(this);
            convertedModels =  new Dictionary<Model, Model>();

            Initialize();
        }

        private new void Initialize()
        {
            base.Initialize();
            Random rand = new Random();
            float radius = rand.Next(3000, 4000);

            for (int i = 0; i < (new Random()).Next(5, 8); i++)
            {
                float theta = (float) rand.NextDouble() * MathHelper.TwoPi;
                this.structures.Add(new Hut(TheGame, this, Globals.PointOnRadius(radius, theta), 50.0, 50.0));
            }

            //this.structures.Add(new Hut(TheGame, this, new Vector2(0, 400), 50.0, 50.0));
        }

        public new void LoadContent()
        {
            //create player
            main = new Player(TheGame, this, Vector3.Zero, 100);
            TheCamera = new PlayerCamera(TheGame);
            Globals.lights = new Lights[Globals.maxLights];           

            Lights.AddLight(new Lights(
                           new Vector3(4000.0f, 200.0f, 0.0f),
                           new Vector4(1.0f),
                           new Vector4(1.0f),
                           new Vector4(1.0f),
                           new Vector4(0.3f, 0.0470588f, 0.0f, 1.0f),
                           new Vector4(1.0f))
                       );
            Lights.AddLight(new Lights(
                           new Vector3(-4000.0f, 200.0f, 0.0f),
                           new Vector4(1.0f),
                           new Vector4(1.0f),
                           new Vector4(1.0f),
                           new Vector4(0.3f, 0.3f, 0.3f, 1.0f),
                           new Vector4(1.0f))
                       );

            TheBackground = TheGame.Content.Load<Texture2D>(@"Textures\sky");

            foreach (Strucure s in structures)
                s.LoadContent();
        }

        public void Unload()
        {
            main.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (TheGame.gameManager.State.Value == TheGame.PlayingState && doOnce)
            {
                Globals.sounds.PlayCue("LavaBubbles");
                doOnce = false;
            }
            //need to update camera
            TheCamera.Update(gameTime);

            //and the main
            main.Update(gameTime);

            TheCollisionManager.Update(gameTime);
            TheCollisionManager.ManageCollisions();

            //and all the enemies
            foreach (Enemy e in enemies)
                e.Update(gameTime);

            List<Enemy> temp = new List<Enemy>();
            foreach (Enemy e in enemies)
                temp.Add(e);

            foreach (Enemy e in temp)
                if (e.Health <= 0)
                    enemies.Remove(e);

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

            TheGame.SpriteBatch.Begin();
            Rectangle rect = new Rectangle(0, 0, TheGame.GraphicsDevice.PresentationParameters.BackBufferWidth,
                TheGame.GraphicsDevice.PresentationParameters.BackBufferHeight);
            TheGame.SpriteBatch.Draw(TheBackground, rect, Color.White);

            TheGame.SpriteBatch.End();

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
