using UnityEngine;

namespace Bodardr.Utility.Runtime
{
    public static class GameObjectExtensions
    {
        /// <returns>True if this GameObject is in the <b>DontDestroyOnLoad</b> Scene</returns>
        public static bool IsDontDestroyOnLoad(this GameObject gameObject) => gameObject.scene.name == "DontDestroyOnLoad";
        
        /// <returns>The full scene path of this GameObject</returns>
        public static string GetFullPath(this GameObject gameObject) => gameObject.transform.GetFullPath();
    }
}