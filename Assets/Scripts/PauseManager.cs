using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    public void PauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Game is paused
            Time.timeScale = 0f;
        }
        else
        {
            // Game is unpaused
            Time.timeScale = 1f;
        }
    }
}