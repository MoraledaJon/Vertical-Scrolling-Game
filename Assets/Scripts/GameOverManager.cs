using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private int coin;
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
        int startCoin = 0;

        score = finalScore;
        ScoreManager.Instance.AddScore(score);

        List<int> highScoresList = ScoreManager.Instance.GetHighScores();
        int highScore = highScoresList[0];

        coin = CoinManager.Instance.GetCurrentGameCoins();
        CoinManager.Instance.SaveGame();

        while (timer < animationDuration)
        {
            int interpolatedScore = (int)Mathf.Lerp(startScore, finalScore, timer / animationDuration);

            int interpolatedScoreHighScore = (int)Mathf.Lerp(startScore, highScore, timer / animationDuration);

            int interpolatedCoin = (int)Mathf.Lerp(startCoin, coin, timer / animationDuration);

            scoreText.text = interpolatedScore.ToString();

            bestScore.text = interpolatedScoreHighScore.ToString();

            coinText.text = interpolatedCoin.ToString();

            timer += Time.deltaTime;

            yield return null;
        }

        scoreText.text = score.ToString();
        bestScore.text = highScore.ToString();
        coinText.text = coin.ToString();
    }
}
