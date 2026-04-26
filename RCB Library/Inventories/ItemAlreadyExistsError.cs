using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Inventories
{
    public class ItemAlreadyExistsError : Error
    {
        public ItemAlreadyExistsError(string name) : base(401, "Item Already Exists", $"The item \"{name}\" already exists.") { }
    }
}
