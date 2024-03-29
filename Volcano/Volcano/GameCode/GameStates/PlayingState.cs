﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XELibrary;

namespace Volcano
{
    public sealed class PlayingState : BaseGameState, IPlayingState
    {
        SpriteFont font;
        Random rand;
        Color color;

        public PlayingState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IPlayingState), this);
            rand = new Random();
        }

        /// <summary>
        /// In Playing state, we want to: (1) control the camera, (2) control options
        /// to pause. This update maintains the options to pause.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Input.WasPressed(0, Buttons.Back, Keys.Escape))
            {
                this.Game.Exit();
            }

            // push our paused state onto the stack
            if (Input.WasPressed(0, Buttons.Start, Keys.Enter))
                GameManager.PushState(OurGame.PausedState.Value);

            if (Input.WasPressed(0, Buttons.X, Keys.X))
            {
                //simulate game over
                //randomly pick if we win or lose
                if (rand.Next(2) < 1) //lose 
                    GameManager.PushState(OurGame.LostGameState.Value);
                else //win 
                    GameManager.PushState(OurGame.WonGameState.Value);
            }

            //simulate activity on the game
            //when updating ...
            if (color == Color.Black)
                color = Color.Purple;
            else
                color = Color.Black;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

        }

        protected override void StateChanged(object sender, EventArgs e)
        {
            base.StateChanged(sender, e);

            if (GameManager.State != this.Value)
            {
                Visible = true;
                Enabled = false;
            }
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>(@"Fonts\Arial");
        }
    }
}
