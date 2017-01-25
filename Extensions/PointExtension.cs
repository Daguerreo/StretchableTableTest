using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StretchableTest.Extensions
{
    /// <summary>
    /// Defines extensions for System.Windows.Point baseclass
    /// </summary>
    /// La scelta e' voluta per non ridefinire gli operatori di una classe base
    /// (e concettualmente la somma di due punti non ha senso)
    public static class PointExtension
    {
        /// <summary>
        /// Add point p2 in point p1
        /// </summary>
        public static void Add(this Point p1, Point p2)
        {
            p1.X += p2.X;
            p1.Y += p2.Y;
        }

        /// <summary>
        /// Subtract point p2 in point p1
        /// </summary>
        public static void Sub(this Point a, Point b)
        {
            a.X -= b.X;
            a.Y -= b.Y;
        }

        /// <summary>
        /// Assign to point che x and y value
        /// </summary>
        public static void Assign(this Point point, double x, double y)
        {
            point.X = x;
            point.Y = y;
        }
    }
}
