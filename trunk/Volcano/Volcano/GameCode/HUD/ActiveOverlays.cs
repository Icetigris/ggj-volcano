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
    /// Class for handling all overlays to be actively displayed on
    /// screen.
    /// </summary>
    public class ActiveOverlays
    {
        #region Variables

        protected HUD headsUp;

        protected int stageTime;
        protected int playerPressure;
        protected int playerMaxPressure;

        #endregion
        #region GetSet


        #endregion
        #region Constructors

        public ActiveOverlays(IServiceProvider serviceProvider, GraphicsDevice device)
        {
            headsUp = new HUD(serviceProvider, device);
            playerPressure = 100;
            playerMaxPressure = 100;

            //this needs to be dependant on data from stage. But for now
            //we do as so. Also time is in total seconds. Divide by 60
            //to get a minute.
            stageTime = 300;
        }

        #endregion
        #region Initialization

        private void Initialize() { }

        #endregion

        public void LoadContent()
        {
            headsUp.LoadContent();
        }

        #region Update

        public void Update(GameTime gameTime, Stage stage)
        {
            playerPressure = stage.main.Pressure;
            playerMaxPressure = stage.main.MaxPressure;
            headsUp.Update(gameTime, playerPressure, playerMaxPressure);
        }
        #endregion
        #region Methods
        #endregion
        #region Draw

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            headsUp.Draw(spriteBatch, gameTime);
        }

        #endregion
    }
}
