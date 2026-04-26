using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Entity
{
    public class EntityAlreadyExistsError : Error
    {
        public EntityAlreadyExistsError(string name) : base(301, "Entity Already Exists", $"The entity \"{name}\" already exists.") { }
    }
}
