using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Entity
{
    public class InvalidEntityNameError : Error
    {
        public InvalidEntityNameError(string name) : base(300, "Invalid Entity Name", $"The entity name \"{name}\" was invalid.") { }
    }
}
