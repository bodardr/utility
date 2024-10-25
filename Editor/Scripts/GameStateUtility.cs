using UnityEditor;


public namespace Bodardr.Utility.Editor
{
    
public static class GameStateUtility
{
    [MenuItem("Game State/Invoke Start")]
    public static void InvokeStart()
    {
        GameStateEvents.Invoke(GameState.START);
    }

    [MenuItem("Game State/Invoke Restart")]
    public static void InvokeRestart()
    {
        GameStateEvents.Invoke(GameState.RESTART);
    }

    [MenuItem("Game State/Invoke Resume")]
    public static void InvokeResume()
    {
        GameStateEvents.Invoke(GameState.RESUME);
    }

    [MenuItem("Game State/Invoke Pause")]
    public static void InvokePause()
    {
        GameStateEvents.Invoke(GameState.PAUSE);
    }

    [MenuItem("Game State/Invoke End")]
    public static void InvokeEnd()
    {
        GameStateEvents.Invoke(GameState.END);
    }

    [MenuItem("Game State/Invoke Game Over")]
    public static void InvokeGameOver()
    {
        GameStateEvents.Invoke(GameState.GAMEOVER);
    }

    public static void Invoke(GameState state)
    {
        GameStateEvents.Invoke(state);
    }
}
}
