using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public Text highScoreText;
    public Text healthText;

    private int score     = 0;
    private int highScore = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        scoreText.text = "SCORE: " + score.ToString();
        highScoreText.text = "HIGHSCORE: " + highScore.ToString();
    }

    void Update()
    {
    }

    public void AddPoints(int x) {
        score += x;
        scoreText.text = "SCORE: " + score.ToString();
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
            highScoreText.text = "HIGHSCORE: " + highScore.ToString();
        }
    }

    public void UpdateHealth(int h)
    {
        healthText.text = "HEALTH: " + h.ToString();
    }
}
