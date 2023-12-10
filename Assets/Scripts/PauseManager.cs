using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public Animator panelAnimator;
    
    public PlayerBehavior playerBehavior;
    public LineDrawer lineDrawer;
    public SpawnManager spawnManager;

    public void PauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            playerBehavior.rb.simulated = false;
            lineDrawer.canDraw = false;
            spawnManager.PauseSpawning();
            panelAnimator.SetBool("IsPaused", true);
        }
        else
        {
            playerBehavior.rb.simulated = true;
            lineDrawer.canDraw = true;
            spawnManager.ResumeSpawning();
            panelAnimator.SetBool("IsPaused", false);
        }
    }
}