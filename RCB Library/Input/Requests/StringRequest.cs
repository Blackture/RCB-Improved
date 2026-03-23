using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Input.Requests
{
    public class StringRequest : InputRequest
    {
        public string Value = "";
        public StringRequest(string info = "") : base(INPUT_TYPE.STR, info)
        {
        }
    }
}
