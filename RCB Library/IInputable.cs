using RCBLibrary.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary
{
    public interface IInputable
    {
        public void Input();
        public void InputCallback(InputRequest ir);
    }
}
