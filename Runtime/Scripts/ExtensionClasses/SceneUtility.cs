using UnityEngine;

namespace Bodardr.Utility.Runtime
{
    public class SceneUtility : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            TransitionCanvas.ChangeScene(sceneName);
        }
    }
}