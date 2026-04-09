using RCBImprovedC;
using RCBLibrary.Characters;
using RCBLibrary.Events;
using RCBLibrary.Input;
using RCBLibrary.Input.Errors;
using RCBLibrary.Input.Requests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RCBLibrary.Menus
{
    public class CharacterMenu : Menu, IUIElement, IInputable
    {
        private Event onCharacterChanged = new Event();

        private Character character;
        public Character Character => character;

        public CharacterMenu() : base("Character Menu")
        {
            onCharacterChanged.AddListener(OnCharacterChanged);
        }

        public void Initialize()
        {
            character = new Character("Placeholder");
        }

        public override void Render()
        {
            Debug.WriteLine("Internal Character Menu Renderer");
        }

        public override void Input()
        {
            if (UIManager.Instance.CurrentElement != this) return;

            CharacterRequest r = new CharacterRequest(character, "Character Menu");
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

            ProcessInput(c!);
        }

        private void ProcessInput(Character input)
        {
            if (input == null) return;
            character = input;
            onCharacterChanged.Invoke();
            Input();
        }

        public virtual void OnCharacterChanged()
        {
            
        }
    }
}
