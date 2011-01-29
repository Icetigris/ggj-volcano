using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.IO;

namespace GameJam_Library
{
    /// <summary>
    /// Timer class for easy use of timers. Things such as stopwatches
    /// and elapsed time.
    /// </summary>
    public class Timer
    {
        #region Variables

        public GameTime GameTime { get; private set; }

        private static Random rand;

        public float Offset { get; private set; }

        public double ElapsedTime { get; private set; }

        public double ElapsedGameTimeSeconds
        {
            get { return GameTime.ElapsedGameTime.TotalSeconds; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a timer with an offset randomly generated
        /// within a predefined range.
        /// </summary>
        public Timer()
        {
            if (Timer.rand == null)
                Timer.rand = new Random();

            Offset = rand.Next(3);
        }
        /// <summary>
        /// Timer with a user-defined offset. Pass in 0
        /// for no offset.
        /// </summary>
        /// <param name="offset"></param>
        public Timer(int offset)
        {
            if (Timer.rand == null)
                Timer.rand = new Random();

            Offset = offset;
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates GameTime of timer. Vital to use!
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            this.GameTime = gameTime;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Returns true when target time has elapsed. Time is in seconds.
        /// </summary>
        /// <param name="targetTime"></param>
        public bool TargetTimeSeconds(float targetTime)
        {
            if (ElapsedTime >= targetTime)
            {
                ElapsedTime = 0.0f;
                return true;
            }
            if (ElapsedTime != targetTime)
            {
                ElapsedTime += GameTime.ElapsedGameTime.TotalSeconds;
                return false;
            }
            return false;
        }

        /// <summary>
        /// Resets the elapsed time of the timer.
        /// </summary>
        public void ResetElapsedTime()
        {
            ElapsedTime = 0.0f;
        }

        #endregion
    }
}


