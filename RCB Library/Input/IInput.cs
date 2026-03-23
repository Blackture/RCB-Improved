using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Input
{
    public interface IInput
    {
        void OnInputRequest(InputRequest ir);
    }
}
