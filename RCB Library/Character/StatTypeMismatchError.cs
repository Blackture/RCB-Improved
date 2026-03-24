using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Character
{
    public class StatTypeMismatchError : Error
    {
        private string errorInput = "";

        public StatTypeMismatchError(string name, STAT_TYPE expected, Type got) : base(302, "Stat Type Mismatch Error", "", true)
        {
            errorInput = $"Stat {name} is of type of {expected.ToString()} but got {got.ToString()}.";
        }

        public override void Send()
        {
            message = $"{errorInput}";
            base.Send();
        }
    }
}
