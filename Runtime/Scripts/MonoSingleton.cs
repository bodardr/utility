using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get => instance ? instance : instance = FindObjectOfType<T>(true);
        private set => instance = value;
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}