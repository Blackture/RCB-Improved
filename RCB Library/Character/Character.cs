using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Character
{
    public class Character : IStats
    {
        private string name;
        private Stats stats;

        public string Name => name;

        public Character(string name)
        {
            this.name = name;
            stats = new Stats();
        }

        public void SetName(string name)
        {
            if (name.Length <= 2) return;
            this.name = name;
        }

        public Stat? GetStat(string name)
        {
            return stats.GetStat(name);
        }

        public T? GetValue<T>(string name) where T : class
        {
            return stats.GetValue<T>(name);
        }

        public void SetValue<T>(string name, T value)
        {
            stats.SetValue<T>(name, value);
        }
    }
}
