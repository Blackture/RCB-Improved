using RCBLibrary.Characters.StatTypes;
using RCBLibrary.Events;
using RCBLibrary.Raycast.Axis;
using RCBLibrary.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RCBLibrary.SceneManagement
{
    public abstract class Scene : IUIElement, IInputable
    {
        private bool isProcedural = false;
        private int index;

        protected bool isActive = false;

        private string key;
        private UI_ELEMENT_TYPE type;
        public string Key => key;
        public UI_ELEMENT_TYPE Type => type;

        public bool IsActive => isActive;
        public bool IsProcedural => isProcedural;
        public int Index => index;

        public Event<MapData> OnRender = new Event<MapData>();

        public Scene(int index, string? key = null, bool isProcedural = false)
        {
            this.index = index;
            this.isProcedural = isProcedural;
            type = UI_ELEMENT_TYPE.SCENE;
            this.key = key ?? $"Scene {index}";
        }

        public abstract void Render();

        public abstract void Show();

        public abstract void Input();

        public abstract void InputCallback(InputRequest ir);
    }
}
