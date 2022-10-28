using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class DictionaryExtensions
{
    public static void Serialize<K, V>(this Dictionary<K, V> dict, out string[] keys, out string[] values)
    {
        SerializeCollection(dict.Keys, out keys);
        SerializeCollection(dict.Values, out values);
    }

    private static void SerializeCollection(ICollection collection, out string[] values)
    {
        values = new string[collection.Count];

        var i = 0;

        foreach (var key in collection)
        {
            values[i] = Serialize(key);
            i++;
        }
    }

    public static Dictionary<Key, Val> Deserialize<Key, Val>(string[] keysJson, string[] valuesJson)
    {
        var keys = Array.ConvertAll(keysJson, DeserializeValue<Key>);
        var values = Array.ConvertAll(valuesJson, DeserializeValue<Val>);

        return keys.Zip(values, (k, v) => new { k, v }).ToDictionary(x => x.k, y => y.v);
    }

    private static string Serialize<T>(T input)
    {
        if (input is string s)
            return s;

        if (input is not ICollection enumerable)
            return typeof(T).IsPrimitive ? input.ToString() : JsonUtility.ToJson(input);

        //Serialize for collections.
        SerializeCollection(enumerable, out var values);

        StringBuilder str = new StringBuilder("[\n");

        foreach (var value in values)
            str.Append(value + ",\n");

        //We remove the last ','
        str.Remove(str.Length - 1, 1);
        str.Append("\n]");

        return str.ToString();
    }

    private static T DeserializeValue<T>(string text)
    {
        var type = typeof(T);

        if (type == typeof(string))
            return (T)(object)text;

        if (type == typeof(bool))
            return (T)(object)string.Equals(text, "True", StringComparison.InvariantCultureIgnoreCase);

        if (typeof(ICollection).IsAssignableFrom(type))
            return (T)DeserializeCollection(text, type);

        return (T)(type.IsPrimitive ? Convert.ChangeType(text, type) : JsonUtility.FromJson(text, type));
    }

    private static object DeserializeValue(string text, Type type)
    {
        if (type == typeof(string))
            return text;

        if (type == typeof(bool))
            return string.Equals(text, "True", StringComparison.InvariantCultureIgnoreCase);

        if (typeof(ICollection).IsAssignableFrom(type))
            return DeserializeCollection(text, type);

        return type.IsPrimitive ? Convert.ChangeType(text, type) : JsonUtility.FromJson(text, type);
    }

    private static ICollection DeserializeCollection(string text, Type collectionType)
    {
        //We trim the array delimiters.
        text = text.Trim('[', ']');

        var isArray = collectionType.IsArray;
        var elementType = isArray ? collectionType.GetElementType() : collectionType.GetGenericArguments()[0];

        Stack<int> braces = new Stack<int>();
        int curElementStartIndex = 0;

        var dynamicListType = typeof(List<>).MakeGenericType(elementType);
        var collection = (IList)Activator.CreateInstance(dynamicListType);

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '{')
                braces.Push(i);
            else if (text[i] == '}')
            {
                if (braces.Count < 1)
                {
                    Debug.LogError(
                        $"Error while parsing \"{text[..Math.Max(text.Length, 10)]}...\" for type {collectionType.FullName}");
                    return default;
                }

                braces.Pop();
            }
            //If there are no braces, then we are at the topmost element
            else if (braces.Count == 0 && text[i] == ',')
            {
                collection.Add(DeserializeValue(text[curElementStartIndex..i], elementType));

                //The next element's start index is assigned to the character after the comma separator.
                curElementStartIndex = i + 1;
            }
        }

        if (!isArray)
            return collection;

        var array = Array.CreateInstance(elementType, collection.Count);
        collection.CopyTo(array, 0);
        return array;
    }
}