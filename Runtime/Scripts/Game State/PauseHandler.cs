using UnityEngine;

public static class PauseHandler
{
    private static bool isPaused = false;

    public static bool IsPaused
    {
        get => isPaused;
        set
        {
            isPaused = value;

            if (value)
                Pause();
            else
                UnPause();

            AudioListener.pause = isPaused;
        }
    }

    public static void TogglePause()
    {
        IsPaused = !IsPaused;
    }

    private static void Pause()
    {
        GameStateEvents.Invoke(GameState.PAUSE);
        Time.timeScale = 0;
    }

    private static void UnPause()
    {
        GameStateEvents.Invoke(GameState.RESUME);
        Time.timeScale = 1;
    }
}