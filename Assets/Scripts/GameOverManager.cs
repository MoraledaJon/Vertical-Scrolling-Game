using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.ShaderKeywordFilter;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI bestScore;
    public float animationDuration = 2.0f;
    public int finalScore;
    public bool isGameOver = false;
    private int score;
    public static GameOverManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverCanvas.SetActive(true);
        StartCoroutine(CountScore());
    }

    IEnumerator CountScore()
    {
        float timer = 0;
        int startScore = 0;
        score = finalScore;

        ScoreManager.Instance.AddScore(score);

        List<int> highScoresList = ScoreManager.Instance.GetHighScores();

        int highScore = highScoresList[0];

        while (timer < animationDuration)
        {
            int interpolatedScore = (int)Mathf.Lerp(startScore, finalScore, timer / animationDuration);

            int interpolatedScoreHighScore = (int)Mathf.Lerp(startScore, highScore, timer / animationDuration);

            scoreText.text = interpolatedScore.ToString();

            bestScore.text = interpolatedScoreHighScore.ToString();

            timer += Time.deltaTime;

            yield return null;
        }

        scoreText.text = score.ToString();
        bestScore.text = highScore.ToString();
    }
}
