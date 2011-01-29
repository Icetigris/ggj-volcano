using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Volcano
{
    /// <summary>
    /// Represents a polygon.
    /// </summary>
    public class Polygon
    {
        /// <summary>
        /// The lines that make up the sides of the polygon.
        /// </summary>
        public List<Line> Lines { get; set; }

        /// <summary>
        /// Tests if the given point is inside the polygon.
        /// </summary>
        /// <param name="point">Point to test.</param>
        /// <returns>true if the point is inside; false otherwise.</returns>
        public bool isPointInside(Point point)
        {
            bool inside = false;

            foreach (var side in Lines)
            {
                if (point.Y > Math.Min(side.Start.Y, side.End.Y))
                    if (point.Y <= Math.Max(side.Start.Y, side.End.Y))
                        if (point.X <= Math.Max(side.Start.X, side.End.X))
                        {
                            float xIntersection = side.Start.X + ((point.Y - side.Start.Y) / (side.End.Y - side.Start.Y)) * (side.End.X - side.Start.X);
                            if (point.X <= xIntersection)
                                inside = !inside;

                        }
            }
            return inside;
        }

    }

    /// <summary>
    /// Represents a line.
    /// </summary>
    public class Line
    {
        /// <summary>
        /// Start of the line.
        /// </summary>
        public Vector2 Start;
        /// <summary>
        /// End of the line.  (EERYBODY OFF!)
        /// </summary>
        public Vector2 End;
    }

}
