using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary
{
    public class AudioNotFoundError : Error
    {
        public AudioNotFoundError(string name) : base(202, "Audio Not Found", $"The audio \"{name}\" is not registered or existing.", true)
        {

        }
    }
}
