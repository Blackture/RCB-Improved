using RCBLibrary;
using RCBLibrary.Input;
using RCBLibrary.Input.Requests;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RCBImprovedC.Menus
{
    public class CharacterMenu : RCBLibrary.Menus.CharacterMenu
    {
        public int lines = 10;
        public enum SETTING
        {
            NONE,
            NAME,
            GENDER,
            COLOR
        }
        private SETTING active = SETTING.NONE;
        private SETTING hover = SETTING.NAME;
        public SETTING Active
        {
            get => active;
            set => active = value;
        }
        public SETTING Hover
        {
            get => hover;
            set
            {
                if (value == SETTING.NONE) return;
                hover = value;
            }
        }

        public override void Render()
        {
            Console.WriteLine("Character Menu");
            Console.WriteLine();
            if (hover == SETTING.NAME)
            {
                Console.WriteLine($"[ Name: {Character.Name} ]");
                Console.WriteLine($"Gender: {Character.Gender.ToString()}");
                Console.WriteLine($"Color: {Character.Color.ToString()}");
                Console.WriteLine();
                Console.WriteLine("Start [Spacebar]");
                Console.WriteLine("Back [B]");
            }
            else if (hover == SETTING.GENDER)
            {
                Console.WriteLine($"Name: {Character.Name}");
                Console.WriteLine($"[ Gender: {Character.Gender.ToString()} ]");
                Console.WriteLine($"Color: {Character.Color.ToString()}");
                Console.WriteLine();
                Console.WriteLine("Start [Spacebar]");
                Console.WriteLine("Back [B]");
            }
            else if (hover == SETTING.COLOR)
            {
                Console.WriteLine($"Name: {Character.Name}");
                Console.WriteLine($"Gender: {Character.Gender.ToString()}");
                Console.WriteLine($"[ Color: {Character.Color.ToString()} ]");
                Console.WriteLine();
                Console.WriteLine("Start [Spacebar]");
                Console.WriteLine("Back [B]");
            }
        }

        public override void OnCharacterChanged()
        {
            Output.ClearCurrentConsoleLines(0, lines);
            Console.SetCursorPosition(0, 0);
            Render();
        }
    }
}
