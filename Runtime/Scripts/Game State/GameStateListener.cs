using System;
using UnityEngine;
using UnityEngine.Events;

public class GameStateListener : MonoBehaviour
{
    [Serializable]
    public class Entry
    {
        public GameState state;
        public UnityEvent unityEvent;
    }

    [SerializeField]
    private Entry[] entries;

    private void Awake()
    {
        foreach (var entry in entries)
        {
            switch (entry.state)
            {
                case GameState.START:
                    GameStateEvents.OnStart += entry.unityEvent.Invoke;
                    break;
                case GameState.RESTART:
                    GameStateEvents.OnRestart += entry.unityEvent.Invoke;
                    break;
                case GameState.PAUSE:
                    GameStateEvents.OnPause += entry.unityEvent.Invoke;
                    break;
                case GameState.RESUME:
                    GameStateEvents.OnResume += entry.unityEvent.Invoke;
                    break;
                case GameState.END:
                    GameStateEvents.OnEnd += entry.unityEvent.Invoke;
                    break;
                case GameState.GAMEOVER:
                    GameStateEvents.OnGameOver += entry.unityEvent.Invoke;
                    break;
            }
        }
    }
}