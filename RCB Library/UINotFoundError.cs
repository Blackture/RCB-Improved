using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary
{
    public class UINotFoundError : Error
    {
        public UINotFoundError(string uiKey) : base(4, "Menu Not Found", $"The provided ui key \"{uiKey}\" is not existing.", true)
        {

        }
    }
}
