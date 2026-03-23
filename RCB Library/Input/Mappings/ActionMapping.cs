using RCBLibrary.Input.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Input.Mappings
{
    public class ActionMapping : IInputMapping<int,int>
    {
        int IInputMapping<int,int>.Map(int input)
        {
            if (input <= 0 || input >= 5)
            {
                (new InvalidActionMappingError<int>(input)).Send();
            }
            return input;
        }
    }
}
