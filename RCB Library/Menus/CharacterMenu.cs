using RCBImprovedC;
using RCBLibrary.Characters;
using RCBLibrary.Input;
using RCBLibrary.Input.Errors;
using RCBLibrary.Input.Requests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RCBLibrary.Menus
{
    public class CharacterMenu : Menu, IUIElement
    {
        private Character character;

        public CharacterMenu() : base("Character Menu")
        {
            
        }
        public override void Render()
        {
            Debug.WriteLine("Internal Character Menu Renderer");
        }

        public override void Input()
        {
            if (UIManager.Instance.CurrentElement != this) return;

            CharacterRequest r = new CharacterRequest("Character Menu");
            r.Subscribe(InputCallback);
            r.Send();
        }

        public override void InputCallback(InputRequest ir)
        {
            if (UIManager.Instance.CurrentElement != this) return;

            if (ir == null) return;
            Character? c = (ir as CharacterRequest)?.Value;

            if (c == null)
            {
                (new InvalidIntegerInputError<Character?>(c)).Send();
            }

            ProcessInput((Character)c);
        }

        private void ProcessInput(Character input)
        {
            
        }
    }
}
