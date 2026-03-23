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
    public abstract class Menu : IMenu
    {
        private string key;
        public string Key { get => key; }

        public Menu(string key) 
        {
            this.key = key;
        }

        public void Show()
        {
            Render();
            MenuInput();
        }

        public virtual void Render()
        {
            Debug.WriteLine("Should Render but we'll see soon if it's implemented.");
        }

        public abstract void MenuInput();

        protected abstract void MenuInputCallback(InputRequest ir);

        protected abstract void ProcessInput(int input);
    }
}
