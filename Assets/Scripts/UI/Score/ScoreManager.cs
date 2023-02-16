using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Singleton Script for managing the score
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text scoreText;
    public TMP_Text topscoreText;

    private int _score = 0;
    private int _topScore = 0;


    private void Awake()
    {
        instance = this;

        _topScore = PlayerPrefs.GetInt(Constants.SavePrefs.TOPSCORE);
        UpdateUI();
    }

    public void Increase(ushort points)
    {
        _score += points;
        _topScore = _topScore < _score ? _score : _topScore;

        UpdateUI();
    }

    public void SaveTopScore()
    {
        PlayerPrefs.SetInt(Constants.SavePrefs.TOPSCORE, _topScore);
    }

    private void UpdateUI()
    {
        scoreText.text = _score.ToString();
        topscoreText.text = _topScore.ToString();
    }
}

