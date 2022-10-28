using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MonoSingletonInstanceHolder
{
    internal static Dictionary<Scene, HashSet<IMonoSingletonDestroyCallback>> singletonsToDestroy = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeSingletonDestructionCallbacks()
    {
        SceneManager.sceneUnloaded -= DestroyActiveSingletons;
        SceneManager.sceneUnloaded += DestroyActiveSingletons;
    }

    private static void DestroyActiveSingletons(Scene sceneToUnload)
    {
        if (!singletonsToDestroy.ContainsKey(sceneToUnload))
            return;

        foreach (var singleton in singletonsToDestroy[sceneToUnload])
            singleton.DestroyCallback();

        singletonsToDestroy[sceneToUnload].Clear();
        singletonsToDestroy.Remove(sceneToUnload);
    }
}