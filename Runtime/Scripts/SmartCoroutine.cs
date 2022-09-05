using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class SmartCoroutine
{
    private readonly bool swapToCoroutinerOnDisabled;

    /// <summary>
    /// The stored IEnumerator function.
    /// Because of lazy evaluation, acts as a sort of pointer to the method.
    /// </summary>
    private Func<IEnumerator> coroutine;

    /// <summary>
    /// The active, currently running coroutine.
    /// </summary>
    private Coroutine internalCoroutine;

    public bool IsRunning { get; private set; }
    public MonoBehaviour Handler { get; set; }

    public static implicit operator bool(SmartCoroutine smartCoroutine) => smartCoroutine.IsRunning;

    public SmartCoroutine(MonoBehaviour handler, Func<IEnumerator> coroutine, bool swapToCoroutinerOnDisabled = true)
    {
        Handler = handler;
        this.coroutine = coroutine;
        this.swapToCoroutinerOnDisabled = swapToCoroutinerOnDisabled;
    }

    public void Start()
    {
        if (!Handler || !Handler.enabled)
            if (swapToCoroutinerOnDisabled)
                Coroutiner.Instance.StartCoroutine(MainCoroutine());
#if UNITY_EDITOR
            else
                Debug.LogError($"Cannot start Coroutine : Handler {Handler.gameObject.name} is disabled or null",
                    Handler.gameObject);
#endif

        internalCoroutine = Handler.StartCoroutine(MainCoroutine());
    }

    public void Restart()
    {
        Stop();
        Start();
    }

    public void Stop()
    { 
        if (Handler)
            Handler.StopCoroutine(internalCoroutine);
        else if (swapToCoroutinerOnDisabled)
            Coroutiner.Instance.StopCoroutine(internalCoroutine);
        else
            Debug.LogError("Coroutine couldn't be stopped. There is no Handler nor Coroutiner. Idk how you got there honestly, good job.");

        internalCoroutine = null;
        IsRunning = false;
    }

    private IEnumerator MainCoroutine()
    {
        IsRunning = true;

        
        yield return coroutine();

        IsRunning = false;
    }
}