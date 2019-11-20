using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ComboManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    public TextMeshProUGUI comboText;
    public Image blue;

    public float scoreCount;

    public float pointsPerSecond;
    public int pointsPerObstacles;
    public bool scoreIncreasing;
    public int maxFailStreak;

    private Dictionary<int,float> fillAmoutPerSpeed;
    private Dictionary<int, int> multiScorePerCombo;
    private int currentCombo = 0;
    private int comboMulti = 1;
    private int failStreak = 0;

    private bool justHit = false;

    void Start()
    {
        scoreText.text = "000000000";

        blue.fillAmount = 0.15f;
        fillAmoutPerSpeed = new Dictionary<int, float>
        {
            { 1, 0.0f },
            { 2, 0.27f },
            { 4, 0.38f },
            { 8, 0.5f },
            { 16, 0.62f },
            { 32, 0.76f },
            { 64, 0.86f },
            { 128, 0.99f }
        };

        multiScorePerCombo = new Dictionary<int, int>
        {
            { 1, 1 },
            { 2, 2 },
            { 4, 3 },
            { 8, 4 },
            { 16, 5 },
            { 32, 7 },
            { 64, 8 },
            { 128, 9 }
        };

        blue.fillAmount = 0.0f;
        //update des scores
        scoreText.SetText("" + Mathf.Round(scoreCount).ToString("000000000"));
        comboText.SetText("X " + currentCombo);
        multiText.text = "x" + comboMulti;
    }

    void Update()
    {
        if (scoreIncreasing) scoreCount += (pointsPerSecond * comboMulti * Time.deltaTime);

        scoreText.SetText("" + Mathf.Round(scoreCount).ToString("000000000"));

        if (Input.GetKeyDown(KeyCode.K))
        {
            succees();
        }
    }

    public void hitObstacles()
    {
        if (!justHit)
        {
            justHit = true;
            failStreak++;
            currentCombo = 1;
        }
        
        if (failStreak > maxFailStreak)
        {
            Debug.Log("dead");
            FindObjectOfType<PlayerController>().dead();
        }
        
        if (fillAmoutPerSpeed.ContainsKey(currentCombo))
        {
            //on passe de palier
            int newMulti;
            multiScorePerCombo.TryGetValue(currentCombo, out newMulti);
            comboMulti = newMulti;
            multiText.text = "x" + comboMulti;

            float fill;
            fillAmoutPerSpeed.TryGetValue(currentCombo, out fill);
            blue.fillAmount = fill;

        }

        //update des scores
        scoreText.SetText("" + Mathf.Round(scoreCount).ToString("000000000"));
        comboText.SetText("X " + currentCombo);
    }

    public void succees()
    {
        if (justHit)
        {
            justHit = false;
        }
        else
        {
            failStreak = 0;
            currentCombo++;
            if (fillAmoutPerSpeed.ContainsKey(currentCombo))
            {
                //on passe de palier
                int newMulti;
                multiScorePerCombo.TryGetValue(currentCombo, out newMulti);
                comboMulti = newMulti;
                multiText.text = "x" + comboMulti;

                float fill;
                fillAmoutPerSpeed.TryGetValue(currentCombo, out fill);
                blue.fillAmount = fill;

            }

            //update des scores
            scoreCount += pointsPerObstacles * comboMulti;
            scoreText.SetText("" + Mathf.Round(scoreCount).ToString("000000000"));
            comboText.SetText("X " + currentCombo);
        }
    }
}
