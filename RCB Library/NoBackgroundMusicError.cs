using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary
{
    public class NoBackgroundMusicError : Error
    {
        public NoBackgroundMusicError() : base(201, "No Background Music Found", $"Something went wrong. There should be background music built in.", false)
        {

        }
    }
}
