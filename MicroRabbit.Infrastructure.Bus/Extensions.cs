using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroRabbit.Infrastructure.Bus
{
    public static class Extensions
    {
        public static void AddItemIfMisssing(this List<Type> list, Type type)
        {
            if(!list.Contains(type)) list.Add(type);
        }

        public static void AddKeyIfMissing(this Dictionary<string, List<Type>> dictionary, string eventName)
        {
            if(!dictionary.ContainsKey(eventName)) dictionary.Add(eventName, new List<Type>());
        }

        public static bool ContainsType(this List<Type> list, Type type)
        {
            return list.Any(s => s.GetType() == type);
        }
    }
}
