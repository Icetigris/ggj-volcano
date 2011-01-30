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
        /// Tests if a line hits the polygon.
        /// </summary>
        /// <param name="b">The other line.</param>
        /// <returns>true if the line hits the polygon; false otherwise.</returns>
        public bool doesLineHitPoly(Line b)
        {
            foreach (var a in Lines)
            {
                double ma = (a.Start.Y - a.End.Y) / (a.Start.X - a.End.X);
                double mb = (b.Start.Y - b.End.Y) / (b.Start.X - b.End.X);
                double xa1 = a.Start.X;
                double xb1 = b.Start.X;
                double ya1 = a.Start.Y;
                double yb1 = b.Start.Y;

                double x = (ma * xa1 - mb * xb1 + yb1 - ya1) / (ma - mb);
                //WOW THAT WAS A LOT OF MATH

                bool isOnA = ((x < a.Start.X) && (x > a.End.X)) || ((x > a.Start.X) && (x < a.End.X));
                bool isOnB = ((x < b.Start.X) && (x > b.End.X)) || ((x > b.Start.X) && (x < b.End.X));
                //WOW THAT WAS SIMPLE

                if (isOnA && isOnB)
                    return true;
            }

            return false;
        }

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
        public Line() { }
        public Line(Vector2 start, Vector2 end) { Start = start; End = end; }

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
