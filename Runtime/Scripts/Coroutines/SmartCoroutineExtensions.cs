using System.Collections;
using UnityEngine;

namespace Bodardr.Utility.Runtime
{
    public static class SmartCoroutineExtensions
    {
        public static SmartCoroutine ToSmartCoroutine(this IEnumerator enumerator, MonoBehaviour handler,
            bool swapToCoroutinerOnDisable = true)
        {
            return new SmartCoroutine(handler, () => enumerator, swapToCoroutinerOnDisable);
        }
    }
}