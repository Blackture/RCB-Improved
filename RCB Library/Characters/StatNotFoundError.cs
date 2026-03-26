using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Characters
{
    public class StatNotFoundError : Error
    {
        public StatNotFoundError(string name) : base(301,"Stat Not Found",$"Stat \"{name}\" was not found.", true)
        {
        }
    }
}
