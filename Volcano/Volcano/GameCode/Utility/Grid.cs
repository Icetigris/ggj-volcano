using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Volcano.GameCode.Utility
{
    class Grid
    {
        /// <summary>
        /// The number of rows in the grid
        /// </summary>
        private int rows;

        /// <summary>
        /// The number of columns in the grid
        /// </summary>
        private int cols;

        /// <summary>
        /// A grid representing the presence of rocks: [row, col]
        /// </summary>
        private bool[,] rocks;

        public Grid(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            this.rocks = new bool[rows, cols];
        }

        public void add(int row, int col)
        {
            this.rocks[row, col] = true;
        }

        public void rem(int row, int col)
        {
            this.rocks[row, col] = false;
        }

        public void toggle(int row, int col)
        {
            this.rocks[row, col] = (!this.rocks[row, col]);
        }

        public void clear()
        {
            this.rocks = new bool[this.rows, this.cols];
        }
    }
}
