using System;
using System.Collections.Generic;

namespace Bodardr.Utility.Runtime
{
    public class UnityDispatcher : DontDestroyOnLoad<UnityDispatcher>
    {
        private static readonly Queue<Action> actionQueue = new();

        private void Update()
        {
            while (actionQueue.Count > 0)
                actionQueue.Dequeue()();
        }

        public static void EnqueueOnUnityThread(Action action)
        {
            if (Instance)
                actionQueue.Enqueue(action);
        }
    }
}