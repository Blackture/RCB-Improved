using RCBLibrary.Math;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RCBLibrary.Characters
{
    public class Character : IStats
    {
        private string name;
        private GENDER gender;
        private Color color = Color.FromKnownColor(KnownColor.Magenta);
        private Vector2 position;
        private Stats stats;

        public string Name => name;
        public GENDER Gender 
        {
            get => gender;
            set => gender = value;
        }
        public Color Color
        {
            get => color;
            set => color = value;
        }
        public Vector2 Position => position;

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
