using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Domain.Core.Extendsions
{
    public static class Extensions
    {
        public static void AddIfNotExists(this List<Type> list, Type type)
        {
            if(!list.Contains(type)) list.Add(type);
        }  
        
        public static void AddKeyIfNotExists(this Dictionary<string, List<Type>> dictionary, string eventName)
        {
            if(!dictionary.ContainsKey(eventName)) dictionary.Add(eventName,new List<Type>());
        }
    }
}
