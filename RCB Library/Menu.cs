using RCBLibrary.Input;
using RCBLibrary.Input.Errors;
using RCBLibrary.Input.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace RCBLibrary
{
    public abstract class Menu : IUIElement, IInputable
    {
        private string key;
        private UI_ELEMENT_TYPE type;
        public string Key { get => key; }
        public UI_ELEMENT_TYPE Type { get => type; }

        public Menu(string key) 
        {
            this.key = key;
            type = UI_ELEMENT_TYPE.MENU;
        }

        public void Show()
        {
            Render();
        }

        public virtual void Render()
        {
            Debug.WriteLine("Should Render but we'll see soon if it's implemented.");
        }

        public abstract void Input();

        public abstract void InputCallback(InputRequest ir);
    }
}
