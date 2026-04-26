using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Inventories
{
    public class InvalidItemNameError : Error
    {
        public InvalidItemNameError(string name) : base(400, "Invalid Item Name", $"The item name \"{name}\" was invalid.") { }
    }
}
