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
    /// <summary>
    /// Characters are living entities that can live and attack. This
    /// includes the player and enemies. Characters have: health, a state of life,
    /// position. They also have a model and a matrix. Designed to be very simplistic.
    /// </summary>
    public class Character : DrawableGameComponent
    {
        #region variables

        public ContentManager TheContent { get; protected set; }
        public GraphicsDeviceManager TheGraphics { get; private set; }

        public Stage TheStage { get; private set; }

        public Vector3 Position { get; protected set; }
        public bool IsAlive { get; protected set; }
        public int Health { get; protected set; }

        public Model TheModel { get; protected set; }
        public Matrix TheRotation { get; protected set; }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public Character(MainGame mainGame,Stage stage ,Vector3 pos, int health)
            :base(mainGame)
        {
            TheContent = new ContentManager(mainGame.Services, "Content");
            TheGraphics = mainGame.graphics;
            TheStage = stage;

            Position = pos;
            Health = health;
            IsAlive = true;

            TheRotation = Matrix.Identity;
        }

        public virtual new void UnloadContent() { }

        public virtual new void Update(GameTime gameTime) { }

        public virtual new void Draw(GameTime gameTime) { }
    }
}
