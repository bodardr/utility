using System;
using System.Collections;
using System.Collections.Generic;

public static class EnumCache
{
    private static readonly Dictionary<Type, object> enumCache = new();
    private static readonly Dictionary<Type, IDictionary> enumStringsCache = new();

    public static T[] GetValues<T>() where T : Enum
    {
        var type = typeof(T);
        if (enumCache.TryGetValue(type, out var values))
            return values as T[];

        var array = Enum.GetValues(type);
        enumCache.Add(type, array);
        return array as T[];
    }

    public static string ToStringCached<T>(this T enumValue) where T : Enum
    {
        var type = typeof(T);

        if (!enumStringsCache.ContainsKey(type))
        {
            T[] values = GetValues<T>();
            
            var dict = new Dictionary<T,string>();
            foreach (var val in values)
                dict.Add(val, val.ToString());
            
            enumStringsCache.Add(type, dict);
        }

        return ((Dictionary<T,string>)enumStringsCache[type])[enumValue];
    }
}
