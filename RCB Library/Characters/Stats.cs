using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using RCBLibrary.Characters.StatTypes;

namespace RCBLibrary.Characters
{
    public class Stats : IStats
    {
        private static List<Stat> allStats = new List<Stat>()
        {
            new FloatStat("Health", "Determines how much damage a character can take before dying.", 100),
            new IntStat("Agility", "Determines how fast a character can move and how likely they are to dodge attacks.", 10),
            new IntStat("Strength", "Determines how much damage a character can deal with physical attacks.", 10),
            new IntStat("Stamina", "Determines how long a character can perform physical activities before getting tired.", 10),
            new IntStat("Educability", "Determines how quickly a character can learn new skills and abilities.", 10),
            new ElementStat("Element", "Determines the elemental affinity of a character, which can affect their strengths and weaknesses against other characters.", ELEMENT.Water),
            new IntStat("Stunned", "Determines how many turns a character is stunned for, which can prevent them from taking actions.", 0)
        };

        private List<Stat> stats = new List<Stat>();

        public Stats()
        {
            stats.AddRange(allStats);
        }

        public static void Initialize(params Stat[] stats)
        {
            allStats.AddRange(stats);
        }

        public Stat? GetStat(string name)
        {
            return stats.Find(stats => stats.Name == name);
        }

        private static Dictionary<STAT_TYPE, Type> types = new Dictionary<STAT_TYPE, Type>()
        {
            { STAT_TYPE.INT, typeof(int) },
            { STAT_TYPE.FLOAT, typeof(float) },
            { STAT_TYPE.ELEMENT, typeof(ELEMENT) },
            { STAT_TYPE.BOOL, typeof(bool) }
        };

        public T? GetValue<T>(string name) where T : class
        {
            Stat? stat = GetStat(name);
            if (stat == null)
            {
                new StatNotFoundError(name).Send();
                return null;
            }
            if (types.ContainsKey(stat.Type) && typeof(T) == types[stat.Type])
            {
                return (T)stat.Value;
            }
            else return null;
        }

        public void SetValue<T>(string name, T value)
        {
            Stat? stat = GetStat(name);
            if (stat == null)
            {
                new StatNotFoundError(name).Send();
                return;
            }
            if (types.ContainsKey(stat.Type) && typeof(T) == types[stat.Type])
            {
                stat.Set(value);
            }
            else
            {
                new StatTypeMismatchError(name, stat.Type, typeof(T)).Send();
            }
        }
    }
}
