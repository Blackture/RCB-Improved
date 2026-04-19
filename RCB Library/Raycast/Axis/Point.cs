using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCBLibrary.Math;

namespace RCBLibrary.Raycast.Axis
{
    public struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        public static implicit operator Vector2(Point p)
        {
            return new Vector2(p.X, p.Y);
        }


        public override readonly bool Equals(object? obj)
        {
            return obj is Point p && this == p;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
