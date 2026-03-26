using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Characters.StatTypes
{
    internal class FloatStat : Stat
    {
        public new float Value
        {
            get
            {
                return (float)base.Value;
            }
            set
            {
                Set(value);
            }
        }

        public FloatStat(string name, string description, float value) : base(name, description, STAT_TYPE.FLOAT)
        {
            Set(value);
        }

        public void Set(float value)
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
