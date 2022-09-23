using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bodardr.Utility.Runtime
{
    public static class ListExtensions
    {
        
        public static T RandomItem<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
        
        
    }
}