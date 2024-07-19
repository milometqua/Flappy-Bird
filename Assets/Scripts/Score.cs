using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;
    public Text scoreText;
    public Text highScoreText;

    private int score;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        scoreText.text = score.ToString();
        highScoreText.text = PlayerPrefs.GetInt("", 0).ToString();
        UpdateHighScore();
    }
    private void UpdateHighScore()
    {
        if(score > PlayerPrefs.GetInt(""))
        {
            PlayerPrefs.SetInt("", score);
            highScoreText.text = score.ToString();
        }
    }    
    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
        UpdateHighScore();
    }
}
