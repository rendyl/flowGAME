using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text hiScoreText;

    public float scoreCount;
    public float hiScoreCount;

    public float pointsPerSecond;
    public bool scoreIncreasing;

    void Start()
    {
        scoreText.text = "My Score : 0 ";
    }

    void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += (pointsPerSecond * Time.deltaTime);
        }

        if (scoreCount > hiScoreCount)
        {
            hiScoreCount = scoreCount;
        }

        scoreText.text = "My Score : " + Mathf.Round(scoreCount);
        hiScoreText.text = "High Score : " + Mathf.Round(hiScoreCount);
    }
}
