using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Characters
{
    public class Stat
    {
        private string name;
        private string description;
        private STAT_TYPE type;

        private object value;
        public virtual object Value
        {
            get {
                return value;
            }
            protected set => this.value = value;
        }
        public STAT_TYPE Type => type;

        public string Name => name;
        public string Description => description;

        public Stat(string name, string description, STAT_TYPE type)
        {
            this.name = name;
            this.description = description;
            this.type = type;
        }

        public virtual void Set(object value)
        {
            this.value = value;
        }
    }
}
