using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary
{
    public class InvalidPathError : Error
    {
        public InvalidPathError(string path) : base(0, "Invalid Path", $"The provided path \"{path}\" is not valid or existing.", true)
        {

        }
    }
}
