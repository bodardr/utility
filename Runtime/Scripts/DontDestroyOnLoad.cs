using UnityEngine;

namespace Bodardr.UI.Runtime
{
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

        private static void CreateInstance()
        {
            GameObject go = new GameObject(typeof(T).Name, typeof(T));
            instance = go.GetComponent<T>();
            DontDestroyOnLoad(go);
        }

        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }
    }
}