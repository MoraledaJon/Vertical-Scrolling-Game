using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public Animator panelAnimator;
    
    public PlayerBehavior playerBehavior;
    public LineDrawer lineDrawer;

    public void PauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            playerBehavior.rb.bodyType = RigidbodyType2D.Static;
            playerBehavior.rbWithData = playerBehavior.rb.velocity;
            playerBehavior.rbAngerVelocity = playerBehavior.rb.angularVelocity;
            lineDrawer.canDraw = false;
            panelAnimator.SetBool("IsPaused", true);
        }
        else
        {
            playerBehavior.rb.bodyType = RigidbodyType2D.Dynamic;
            playerBehavior.rb.velocity = playerBehavior.rbWithData;
            playerBehavior.rb.angularVelocity = playerBehavior.rbAngerVelocity;
            lineDrawer.canDraw = true;
            panelAnimator.SetBool("IsPaused", false);
        }
    }
}