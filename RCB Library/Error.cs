using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary
{
    public abstract class Error
    {
        private int code;
        private string title;
        protected string message;
        private bool fatal;

        public int Code => code;
        public string Title => title;
        public string Message => message;
        public bool Fatal => fatal;

        public Error(int code, string title, string message, bool fatal = false)
        {
            this.code = code;
            this.title = title;
            this.message = message;
            this.fatal = fatal;
        }

        public virtual void Send()
        {
            Game.Instance.Error.Invoke(this);
        }
    }
}
