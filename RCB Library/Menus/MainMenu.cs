using RCBLibrary.Input;
using RCBLibrary.Input.Errors;
using RCBLibrary.Input.Mappings;
using RCBLibrary.Input.Requests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RCBLibrary.Menus
{
    public class MainMenu : Menu, IMenu
    {
        public override void Render()
        {
            Debug.WriteLine("Library MainMenu Render");
        }

        public override void MenuInput()
        {
            IntRequest r = new IntRequest("Main Menu");
            r.Subscribe(MenuInputCallback);
            r.Send();
        }

        protected override void MenuInputCallback(InputRequest ir)
        {
            if (ir == null) return;
            int? i = (ir as IntRequest)?.Value;

            if (i == null)
            {
                (new InvalidIntegerInputError<int?>(i)).Send();
            }

            ProcessInput((int)i);
        }

        protected override void ProcessInput(int input)
        {
            MAIN_MENU_INPUT inp = Input<int, MAIN_MENU_INPUT>.In(new MainMenuMapping(), (int)input);
            switch (inp)
            {
                case MAIN_MENU_INPUT.Start:
                    Debug.WriteLine("Start selected");
                    // Select Menu
                    break;
                case MAIN_MENU_INPUT.Settings:
                    Debug.WriteLine("Settings selected");
                    // Select Settings Menu
                    break;
                case MAIN_MENU_INPUT.Exit:
                    Exit();
                    return;
            }
            MenuInput();
        }

        public virtual void Exit()
        {
            Game.Instance.Stop();
        }
    }
}
