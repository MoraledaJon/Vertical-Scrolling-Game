using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public Animator animator;

    public void GameOver()
    {
        animator.SetBool("gameOver", true);
    }
}
