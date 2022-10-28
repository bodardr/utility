using UnityEngine;

namespace Bodardr.Utility.Runtime
{
    public static class GameObjectExtensions
    {
        /// <returns>True if this GameObject is in the <b>DontDestroyOnLoad</b> Scene</returns>
        public static bool IsDontDestroyOnLoad(this GameObject gameObject)
        {
            return gameObject.scene.name == "DontDestroyOnLoad";
        }
    }
}