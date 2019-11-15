using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    public PlayerController pc;
    public Image blue;

    public float scoreCount;

    public float pointsPerSecond;
    public bool scoreIncreasing;

    public Dictionary<float, float> fillAmoutPerSpeed;
    private float currentSpeed = 1f;

    void Start()
    {
        scoreText.text = "000000000";

        blue.fillAmount = 0.15f;
        fillAmoutPerSpeed = new Dictionary<float, float>
        {
            { 1.2f, 0.27f },
            { 1.4f, 0.38f },
            { 1.6f, 0.5f },
            { 1.8f, 0.62f },
            { 2.0f, 0.76f },
            { 2.2f, 0.86f },
            { 2.4f, 0.99f }
        };
        Debug.Log(fillAmoutPerSpeed.Keys.Count);
    }

    void Update()
    {
        if (scoreIncreasing) scoreCount += (pointsPerSecond * pc.speedMultiplier * Time.deltaTime);

        scoreText.SetText("" + Mathf.Round(scoreCount).ToString("000000000"));
        multiText.SetText("x" + pc.speedMultiplier);

        if (Input.GetKeyDown(KeyCode.K))
        {
            UpdateSpeed();
        }
    }

    void UpdateSpeed()
    {
        currentSpeed += 0.2f;
        float fill;
        Debug.Log(currentSpeed);
        if (!fillAmoutPerSpeed.TryGetValue(currentSpeed, out fill)) Debug.LogError("duck");
        Debug.Log(fill);
        Debug.Log(currentSpeed);
        blue.fillAmount = fill;
    }
}
