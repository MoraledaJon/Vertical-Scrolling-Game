using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Camera cam;
    public TextMeshProUGUI scoreText;

    public int score;

    void Update()
    {
        score = Mathf.RoundToInt(cam.transform.position.y);

        scoreText.text = score.ToString("D4") + "m";
    }

    //public static ScoreManager Instance;
    //public TextMeshProUGUI scoreText;

    //private int score;

    //void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else if (Instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //void Start()
    //{
    //    score = 0;
    //    scoreText.text = score.ToString();
    //}
}
