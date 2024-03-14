using UnityEngine;

public class GameStateInvoker : MonoBehaviour
{
    public void InvokeStart()
    {
        GameStateEvents.Invoke(GameState.START);
    }
    
    public void InvokeRestart()
    {
        GameStateEvents.Invoke(GameState.RESTART);
    }

    public void InvokeResume()
    {
        GameStateEvents.Invoke(GameState.RESUME);
    }

    public void InvokePause()
    {
        GameStateEvents.Invoke(GameState.PAUSE);
    }

    public void InvokeEnd()
    {
        GameStateEvents.Invoke(GameState.END);
    }

    public void InvokeGameOver()
    {
        GameStateEvents.Invoke(GameState.GAMEOVER);
    }
    
    public void Invoke(GameState state)
    {
        GameStateEvents.Invoke(state);
    }
}