using UnityEngine;

namespace Bodardr.Utility.Runtime
{
    /// <summary>
    /// Inherit this class for use in class singletons that are self-initialized
    /// and where its lifecycle consists of the whole session.
    /// This component must be in a scene.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DontDestroyOnLoad<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (!instance)
                    CreateInstance();

                return instance;
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }

        private static void CreateInstance()
        {
            GameObject go = new GameObject(typeof(T).Name, typeof(T));
            instance = go.GetComponent<T>();
            DontDestroyOnLoad(go);
        }
    }
}