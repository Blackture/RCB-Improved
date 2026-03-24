using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Character
{
    public class StatValueOutOfRangeError : Error
    {
        public StatValueOutOfRangeError(int value) : base(300,"Stat Value Out Of Range",$"Stat value {value} is out of range. Value must be non-negative.", true)
        {
        }
        public StatValueOutOfRangeError(float value) : base(300, "Stat Value Out Of Range", $"Stat value {value} is out of range. Value must be non-negative.", true)
        {
        }
    }
}
