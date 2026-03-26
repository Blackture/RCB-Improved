using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Characters
{
    public interface IStats
    {
        Stat? GetStat(string name);

        public T? GetValue<T>(string name) where T : class;

        public void SetValue<T>(string name, T value);
    }
}
