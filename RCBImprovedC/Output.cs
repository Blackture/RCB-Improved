using RCBLibrary.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBImprovedC
{
    public static class Output
    {
        public static void ClearCurrentConsoleLine(int line)
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, line);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        public static void ClearCurrentConsoleLines(int start, int end)
        {
            int currentLineCursor = Console.CursorTop;

            for (int line = start; line <= end; line++)
            {
                Console.SetCursorPosition(0, line);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void ClearPosition(int left, int line)
        {
            Console.SetCursorPosition(left, line);
            Console.Write(" ");
        }

        public static (int left, int line) ConvertTo(Vector2 pos)
        {
            return ((int)pos.X, (int)(Console.BufferHeight - pos.Y));
        }
    }
}
