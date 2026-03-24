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

        public static implicit operator Vector2(Point p)
        {
            return new Vector2(p.X, p.Y);
        }
    }
}
