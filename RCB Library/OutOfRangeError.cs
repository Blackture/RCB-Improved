using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary
{
    public class OutOfRangeError : Error
    {
        public OutOfRangeError() : base(3, "Audio Not Found", $"The number was out of range.", true)
        {

        }

        public OutOfRangeError(float lower, float upper, float number) : base(3, "Audio Not Found", $"The number {number} was out of range. The range was from {lower.ToString("000.000")} to {upper.ToString("000.000")}", true)
        {

        }
    }
}
