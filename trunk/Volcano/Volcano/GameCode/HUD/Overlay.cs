using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Volcano
{
    /// <summary>
    /// This is the base class of screen overlays
    /// such as HealthBars and the like
    /// </summary>
    public class Overlays
    {
        #region Variables

        public ContentManager content { get; private set;}

        public Vector2 position { get; protected set; }

        protected Texture2D PrimaryOverlay;

        protected int width;
        protected int height;

        #endregion

        #region Constructors

        public Overlays(IServiceProvider serviceProvider, GraphicsDevice device)
        {
            content = new ContentManager(serviceProvider, "Content");

            //default that should be overriden
            this.position = new Vector2(30.0f, 30.0f);

            //currently w/h not used
            this.width = 300;
            this.height = 250;
        }

        #endregion
        #region Initialization

        protected virtual void LoadImages() { }

        #endregion
        #region Update
        #endregion
        #region Methods
        #endregion
        #region Draw

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle screenRectangle = new Rectangle(0, 0, this.width, this.height);

            spriteBatch.Draw(PrimaryOverlay, screenRectangle, Color.White);
        }

        #endregion
    }
}
