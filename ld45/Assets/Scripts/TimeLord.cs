using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TimeLord : MonoBehaviour
{
    public static bool GamePaused = false;

    public void PauseGame()
    {
        GamePaused = true;
        Time.timeScale = 0.01f;
    }
    public void UnPauseGame()
    {
        GamePaused = false;
        Time.timeScale = 1f;
    }
    public void TogglePause()
    {
        if (GamePaused) UnPauseGame();
        else PauseGame();

    }
}
