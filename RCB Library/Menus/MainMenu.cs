using RCBImprovedC;
using RCBLibrary.Input;
using RCBLibrary.Input.Errors;
using RCBLibrary.Input.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace RCBLibrary.Menus
{
    public class MainMenu : Menu, IUIElement
    {
        public MainMenu() : base("Main Menu")
        {
        }

        public override void Render()
        {
            Debug.WriteLine("Library Main Menu Render");
        }

        public override void Input()
        {
            if (UIManager.Instance.CurrentElement != this) return;

            IntRequest r = new IntRequest("Main Menu");
            r.Subscribe(InputCallback);
            r.Send();
        }

        public override void InputCallback(InputRequest ir)
        {
            if (UIManager.Instance.CurrentElement != this) return;

            if (ir == null) return;
            int? i = (ir as IntRequest)?.Value;

            if (i == null)
            {
                (new InvalidIntegerInputError<int?>(i)).Send();
            }

            ProcessInput((int)i);
        }

        private void ProcessInput(int input)
        {
            MAIN_MENU_INPUT inp = (MAIN_MENU_INPUT)input;
            switch (inp)
            {
                case MAIN_MENU_INPUT.Singleplayer:
                    UIManager.Instance.ShowElement("Character Menu");
                    Debug.WriteLine("Start selected");
                    // Select Menu
                    break;
                case MAIN_MENU_INPUT.Online:
                    Debug.WriteLine("Online selected");
                    // Select Menu
                    break;
                case MAIN_MENU_INPUT.Settings:
                    UIManager.Instance.ShowElement("Settings Menu");
                    break;
                case MAIN_MENU_INPUT.Exit:
                    Exit();
                    return;
            }
            Input();
        }

        public virtual void Exit()
        {
            Game.Instance.Stop();
        }
    }
}
