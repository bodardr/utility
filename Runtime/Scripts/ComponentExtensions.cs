using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Bodardr.Utility.Runtime
{
    public static class ComponentExtensions
    {
        /// <summary>
        /// Taken inspiration from : https://answers.unity.com/questions/863509/how-can-i-find-all-objects-that-have-a-script-that.html
        /// </summary>
        /// <param name="includeInactive"></param>
        /// <typeparam name="T">The type of component to fetch</typeparam>
        /// <returns>All components across all scenes, containing this class or interface type.</returns>
        public static List<T> FindComponentsInAllActiveScenes<T>(bool includeInactive = true)
        {
            List<T> components = new List<T>();

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                foreach (var rootGO in SceneManager.GetSceneAt(i).GetRootGameObjects())
                    components.AddRange(rootGO.GetComponentsInChildren<T>(includeInactive));
            }

            return components;
        }

        public static List<T> FindComponentsInActiveScene<T>(bool includeInactive = true)
        {
            List<T> components = new List<T>();

            foreach (var rootGO in SceneManager.GetActiveScene().GetRootGameObjects())
                components.AddRange(rootGO.GetComponentsInChildren<T>(includeInactive));

            return components;
        }

        public static List<T> FindComponentsInScene<T>(Scene scene, bool includeInactive = true)
        {
            List<T> components = new List<T>();

            foreach (var rootGO in scene.GetRootGameObjects())
                components.AddRange(rootGO.GetComponentsInChildren<T>(includeInactive));

            return components;
        }
    }
}