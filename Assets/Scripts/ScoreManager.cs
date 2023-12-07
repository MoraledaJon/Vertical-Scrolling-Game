using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Camera cam;
    public TextMeshProUGUI scoreText;

    private int score;

    // Update is called once per frame
    void Update()
    {
        score = Mathf.RoundToInt(cam.transform.position.y);

        scoreText.text = score.ToString("D4") + "m";
    }
}
