using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary
{
    public class InvalidAudioFormatError : Error
    {
        public InvalidAudioFormatError(string path) : base(200, "Invalid Audio Format", $"The provided path \"{path}\" is no valid audio file or is of an incompatible format.", true)
        {

        }
    }
}
