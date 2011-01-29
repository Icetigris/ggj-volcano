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
    /// Overlays Class used to represent the HUD HealthHub
    /// </summary>
    public class PressureBar : Overlays
    {
        #region Variables

        #region Bar Textures
        protected Texture2D SecondaryOverlay;

        protected Texture2D SecondBar_PrimaryOverlay;
        protected Texture2D SecondBar_SecondaryOverlay;
        protected Texture2D ThirdBar_PrimaryOverlay;
        protected Texture2D ThirdBar_SecondaryOverlay;
        #endregion
        #region Player Pressure
        protected int playerPressure;
        protected int playerMaxPressure;
        protected int previousPlayerPressure;
        #endregion
        #region Bar Scale and Percentage
        protected float barOnePercent;
        protected float barLengthScale;
        protected float barHeightScale;
        #endregion

        protected Vector2 secondary_position;

        protected int maxWidth;
        protected int maxHeight;

        protected int secondary_height;
        protected int secondary_width;

        protected int secondary_maxWidth;
        protected int secondary_maxHeight;

        #endregion
        #region Constructors

        public PressureBar(IServiceProvider serviceProvider, GraphicsDevice device)
            : base(serviceProvider, device)
        {
            this.barLengthScale = 1.0f;
            this.barHeightScale = 1.0f;

            //position.Y should be relative to the window size. It is currently
            //slapped with a constant.
            position = new Vector2(5.0f, 5.0f);
            secondary_position = new Vector2(22.0f, 500.0f);

            height = 5;
            width = 120;

            //secondary_height = 500;
            secondary_height = 0;
            secondary_width = 110;
            secondary_maxHeight = 500;

            maxHeight = height;

            playerPressure = 0;
            playerMaxPressure = 100;


            this.Initialize();
        }

        #endregion
        #region Initialization

        private void Initialize()
        {
            FindOneBarPercentage();
        }

        public void LoadContent()
        {
            LoadImages();
        }

        protected override void LoadImages()
        {
            this.PrimaryOverlay = content.Load<Texture2D>(@"Textures\pressureGaugeRock");
            this.SecondaryOverlay = content.Load<Texture2D>(@"Textures\pressureGaugeBar");
        }

        #endregion
        #region Update

        public void Update(GameTime gameTime, int playerPressure, int playerMaxPressure)
        {
            this.previousPlayerPressure = this.playerPressure;
            this.playerPressure = playerPressure;
            this.playerMaxPressure = playerMaxPressure;
            UpdateBarHeight();
        }

        /// <summary>
        /// Adjust the health bar's length in accordance to
        /// the life percentage of the player.
        /// currentLife/MaxLife = percentage.
        /// </summary>
        public void UpdateBarHeight()
        {
            FindLengthScale();

            if (playerPressure > previousPlayerPressure)
            {
                secondary_height = (int)(secondary_height + (barOnePercent * (barHeightScale * 5.5)));
            }
            else if (playerPressure < previousPlayerPressure)
            {
                secondary_height = (int)(secondary_height - (barOnePercent * (barHeightScale * 5.5)));
            }
            else if (secondary_height <= 0)
            {
                secondary_height = 0;
            }
        }

        /// <summary>
        /// Find the value of one percent of the life bar width
        /// width * 0.01
        /// </summary>
        public void FindOneBarPercentage()
        {
            barOnePercent = (secondary_maxHeight * 0.01f);
        }

        /// <summary>
        /// Find the value to scale the health bar by
        /// </summary>
        public void FindLengthScale()
        {
            barLengthScale = (float)playerPressure / (float)playerMaxPressure;
        }

        #endregion
        #region Methods
        #endregion
        #region Draw

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle PrimaryRectangle = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.maxHeight + 550);
            Rectangle SecondaryRectangle = new Rectangle((int)(int)secondary_width, (int)this.maxHeight + 550, -(int)secondary_width + 20, -(int)secondary_height);
          
            //This SecondaryOverlay color may need to change dynamically...
            spriteBatch.Draw(SecondaryOverlay, SecondaryRectangle, Color.OrangeRed);
            spriteBatch.Draw(PrimaryOverlay, PrimaryRectangle, Color.White);
        }

        #endregion
    }
}
