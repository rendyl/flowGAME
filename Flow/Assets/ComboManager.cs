using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ComboManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    public TextMeshProUGUI comboText;
    public Image blue;

    [Header("Score settings")]
    public float scoreCount;
    public float pointsPerSecond;
    public int pointsPerObstacles;
    public bool scoreIncreasing;
    public int maxFailStreak;

    private Dictionary<int,float> fillAmoutPerSpeed;
    private Dictionary<int, int> multiScorePerCombo;
    private int currentCombo = 1;
    private int comboMulti = 1;
    private int failStreak = 0;
    //private float minFloatRadialBar = 0.18f;
    //private float maxFloatRadialBar = 0.94f;

    private bool justHit = false;

    [Header("Particules")]
    public ParticleSystem back;
    public ParticleSystem leftFoot;
    public ParticleSystem rightFoot;

    void Start()
    {
        scoreText.text = "000000000";

        blue.fillAmount = 0.15f;
        fillAmoutPerSpeed = new Dictionary<int, float>
        {
            { 1, 0.18f },
            { 2, 0.27f },
            { 4, 0.38f },
            { 8, 0.5f },
            { 16, 0.62f },
            { 32, 0.76f },
            { 64, 0.86f },
            { 128, 0.94f }
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
        comboText.SetText("X " + (currentCombo-1));
        multiText.text = "x" + comboMulti;
    }

    void Update()
    {
        if (scoreIncreasing) scoreCount += (pointsPerSecond * comboMulti * Time.deltaTime);

        scoreText.SetText("" + Mathf.Round(scoreCount).ToString("000000000"));

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    success();
        //}
    }

    public void hitObstacles()
    {
        if (!justHit)
        {
            justHit = true;
            failStreak++;
            currentCombo = 1;
            back.Stop();
            leftFoot.Stop();
            rightFoot.Stop();
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
        comboText.SetText("X " + (currentCombo-1));
    }

    public void success()
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
                blue.fillAmount = 0.0f;

                float fill;
                fillAmoutPerSpeed.TryGetValue(currentCombo, out fill);
                blue.fillAmount = fill;

                //activation des particules
                if (currentCombo == 64)
                {
                    //activation des pieds
                    leftFoot.Play();
                    rightFoot.Play();
                }
                if(currentCombo==128)
                {
                    //activation du dos
                    back.Play();
                }

            }
            else if (currentCombo < 129)
            {
                //Definition du fill amount en interpolation linéaire entre les paliers
                var Enum = multiScorePerCombo.Keys.GetEnumerator();
                int max = Enum.Current;
                int min = Enum.Current;
                while (max < currentCombo)
                {
                    min = Enum.Current;
                    Enum.MoveNext();
                    max = Enum.Current;
                }

                float fillAmoutMin;
                float fillAmoutMax;
                fillAmoutPerSpeed.TryGetValue(min, out fillAmoutMin);
                fillAmoutPerSpeed.TryGetValue(max, out fillAmoutMax);

                //interpolation entre les paliers par sur la barre entiere
                float fillamout01 = (float)(((float)currentCombo - (float)min) / ((float)max - (float)min));//Radial entre 0 et 1
                float leurp = fillamout01 * (fillAmoutMax - fillAmoutMin);
                float fillamout = fillAmoutMin + leurp;//01 ramene entre paliers min et max 
                blue.fillAmount = fillamout;
            }

            //update des scores
            scoreCount += pointsPerObstacles * comboMulti;
            scoreText.SetText("" + Mathf.Round(scoreCount).ToString("000000000"));
            comboText.SetText("X " + (currentCombo-1));


           

            /*Debug.Log(test);
            Debug.Log(test2);*/

        }
    }
}
