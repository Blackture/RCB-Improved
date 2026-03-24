using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Character.StatTypes
{
    internal class IntStat : Stat
    {
        public new int Value
        {
            get
            {
                return (int)base.Value;
            }
            set
            {
                Set(value);
            }
        }

        public IntStat(string name, string description, int value) : base(name, description, STAT_TYPE.INT)
        {
            Set(value);
        }

        public void Set(int value)
        {
            if (value < 0)
            {
                new StatValueOutOfRangeError(value).Send();
                return;
            }
            base.Set(value);
        }
    }
}
