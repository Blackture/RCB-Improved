using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCBLibrary.Raycast
{
    public class Base
    {
        public static int CurrentX, CurrentY;

        public static void GetVector()
        {
            CurrentX = Console.CursorLeft;
            CurrentY = Console.CursorTop;
        }

        public static void CursorX(int amount)
        {
            GetVector();
            CurrentX += amount;
            Console.SetCursorPosition(CurrentX, CurrentY);
            //erst testen ob er noch eins weiter kann
        }

        public static void CursorY(int amount)
        {
            GetVector();
            CurrentY += amount;
            Console.SetCursorPosition(CurrentX, CurrentY);
            //erst testen ob er noch eins weiter kann
        }
    }
}
