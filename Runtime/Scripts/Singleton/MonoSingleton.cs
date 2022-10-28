using System.Collections.Generic;
using Bodardr.Utility.Runtime;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour, IMonoSingletonDestroyCallback where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (!instance)
                instance = GetInstance();

            return instance;
        }
        private set => instance = value;
    }

    public virtual void DestroyCallback()
    {
        if (!instance)
            instance = null;
    }

    private static T GetInstance()
    {
        var singleton = FindObjectOfType<T>(true);

        var go = singleton.gameObject;

        if (go.IsDontDestroyOnLoad())
            return singleton;

        var singletonsToDestroy = MonoSingletonInstanceHolder.singletonsToDestroy;

        if (!singletonsToDestroy.ContainsKey(go.scene))
            singletonsToDestroy[go.scene] = new HashSet<IMonoSingletonDestroyCallback>();

        singletonsToDestroy[go.scene].Add((IMonoSingletonDestroyCallback)singleton);

        return singleton;
    }
}