using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineExtensions
{
    public static IEnumerator Then(this IEnumerator coroutine, IEnumerator then)
    {
        yield return coroutine;
        yield return then;
    }

    public static IEnumerator Then(this IEnumerator coroutine, Action then)
    {
        yield return coroutine;
        yield return then.ToCoroutine();
    }

    public static IEnumerator ToCoroutine(this Action action)
    {
        action();
        yield break;
    }

    public static IEnumerator Wait(this IEnumerator coroutine, float secondsToWait)
    {
        yield return coroutine;
        yield return new WaitForSeconds(secondsToWait);
    }

    public static IEnumerator Wait(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
    }

    public static IEnumerator WaitForAll(this IEnumerable<YieldInstruction> instructions)
    {
        foreach (var i in instructions)
            yield return i;
    }

    public static IEnumerator WaitForAll(this IEnumerable<IEnumerator> instructions)
    {
        foreach (var i in instructions)
            yield return i;
    }

    public static IEnumerator WaitForAll(this IEnumerable<SmartCoroutine> instructions)
    {
        foreach (var i in instructions)
            yield return new WaitUntil(() => !i.IsRunning);
    }
}