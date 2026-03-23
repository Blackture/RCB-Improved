using RCBLibrary.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RCBLibrary.Input
{
    public class InputRequest
    {
        private INPUT_TYPE type;
        private string info;
        private Event<InputRequest> callback = new Event<InputRequest>();

        public INPUT_TYPE Type => type;
        public string Info => info;

        public InputRequest(INPUT_TYPE type, string info)
        {
            this.type = type;
            this.info = info;
        }

        public void Send()
        {
            if (!Game.Instance.Active) return;

            Game.Instance.Input.Invoke(this);
        }

        public void Subscribe(Action<InputRequest> callback)
        {
            this.callback.AddListener(callback);
        }

        public void Reply()
        {
            callback.Invoke(this);
        }
    }
}
