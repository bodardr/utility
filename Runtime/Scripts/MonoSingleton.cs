using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this as T;
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}