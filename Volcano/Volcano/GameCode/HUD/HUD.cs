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
    /// Class to handle all components of the Heads Up Display
    /// to be exibited on screen.
    /// </summary>
    public class HUD
    {
        #region Variables

        protected PressureBar pressureBar { get; private set; }
        public int playerPressure { get; private set; }
        public int playerMaxPressure { get; private set; }

        #endregion
        #region Constructors

        public HUD(IServiceProvider serviceProvider, GraphicsDevice device)
        {
            pressureBar = new PressureBar(serviceProvider, device);
        }

        #endregion
        #region Initialization

        private void Initialize() { }

        #endregion

        public void LoadContent()
        {
            pressureBar.LoadContent();
        }

        #region Update

        public void Update(GameTime gameTime, int playerPressure, int playerMaxPressure)
        {
            this.playerPressure = playerPressure;
            this.playerMaxPressure = playerMaxPressure;
            pressureBar.Update(gameTime, playerPressure, playerMaxPressure);
        }

        #endregion
        #region Methods
        #endregion
        #region Draw

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            pressureBar.Draw(spriteBatch, gameTime);
        }

        #endregion
    }
}
