using RCBLibrary.Input.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Input.Mappings
{
    public class MainMenuMapping : IInputMapping<int, MAIN_MENU_INPUT>
    {
        MAIN_MENU_INPUT IInputMapping<int, MAIN_MENU_INPUT>.Map(int input)
        {
            return (MAIN_MENU_INPUT)input;
        }
    }
}
