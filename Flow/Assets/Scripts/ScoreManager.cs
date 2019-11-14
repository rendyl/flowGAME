using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    public PlayerController pc;

    public float scoreCount;

    public float pointsPerSecond;
    public bool scoreIncreasing;

    void Start()
    {
        scoreText.text = "Score : 0";
    }

    void Update()
    {
        if (scoreIncreasing) scoreCount += (pointsPerSecond * pc.speedMultiplier * Time.deltaTime);

        scoreText.SetText("Score : " + Mathf.Round(scoreCount));
        multiText.SetText("x" + pc.speedMultiplier);
    }
}
