using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Input.Requests
{
    public class IntRequest : InputRequest
    {
        public int? Value = null;
        public IntRequest(string info = "") : base(INPUT_TYPE.INT, info)
        {
        }

        public void Reply(int input)
        {
            if (input is int i)
            {
                Value = i;
                Reply();
            }
        }
    }
}
