using System.Collections;
using UnityEngine;

namespace Bodardr.Utility.Runtime
{
    public static class ListExtensions
    {
        public static int RandomIndex(this IList list)
        {
            return Random.Range(0, list.Count);
        }
    }
}