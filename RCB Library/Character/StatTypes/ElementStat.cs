using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Character.StatTypes
{
    internal class ElementStat : Stat
    {
        public new ELEMENT Value
        {
            get
            {
                return (ELEMENT)base.Value;
            }
            set
            {
                Set(value);
            }
        }

        public ElementStat(string name, string description, ELEMENT value) : base(name, description, STAT_TYPE.ELEMENT)
        {
            Set(value);
        }

        public void Set(ELEMENT value)
        {
            if ((int)value < 0 || (int)value > 3)
            {
                new StatValueOutOfRangeError((int)value).Send();
                return;
            }
            base.Set(value);
        }
    }
}
