using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get => instance ??= FindObjectOfType<T>();
        private set => instance = value;
    }

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