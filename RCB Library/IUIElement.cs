using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary
{
    public interface IUIElement
    {
        public string Key { get; }
        public UI_ELEMENT_TYPE Type { get; }
        public void Render();
        public void Show();
    }
}
