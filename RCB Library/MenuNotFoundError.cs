using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary
{
    public class MenuNotFoundError : Error
    {
        public MenuNotFoundError(string menuTitle) : base(1, "Menu Not Found", $"The provided menu key \"{menuTitle}\" is not existing.", true)
        {

        }
    }
}
