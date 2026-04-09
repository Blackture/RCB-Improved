using System;
using System.Collections.Generic;
using System.Text;

namespace RCBImprovedC.Menus
{
    public static class GenerationScreen
    {
        public static void GenerationCallback(string s)
        {
            Console.Write(s);
            Output.ClearCurrentConsoleLine(0);
            Console.SetCursorPosition(0, 0);
        }
    }
}
