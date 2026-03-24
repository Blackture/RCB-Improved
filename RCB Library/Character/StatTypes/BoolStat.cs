using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Character.StatTypes
{
    internal class BoolStat : Stat
    {
        public new bool Value
        {
            get
            {
                return (bool)base.Value;
            }
            set
            {
                Set(value);
            }
        }

        public BoolStat(string name, string description, bool value) : base(name, description, STAT_TYPE.BOOL)
        {
            Set(value);
        }
    }
}
