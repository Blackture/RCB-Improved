using RCBLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBImprovedC
{
    public class MainMenu : RCBLibrary.Menus.MainMenu
    {
        public int lines = 5;

        public override void Render()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine();
            Console.WriteLine("Start [S]");
            Console.WriteLine("Options [O]");
            Console.WriteLine("Exit [E]");
        }

        public override void Exit()
        {
            Console.WriteLine("Exiting...");
            base.Exit();
        }
    }
}
