using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    public Camera cam;
    public TextMeshProUGUI scoreText;
    public GameOverManager gameOverManager;

    public int score;

    void Update()
    {
        if(gameOverManager.isGameOver)
        {
            return;
        }

        score = Mathf.RoundToInt(cam.transform.position.y);

        scoreText.text = score.ToString("D4") + "m";

        gameOverManager.finalScore = score;
    }
}
