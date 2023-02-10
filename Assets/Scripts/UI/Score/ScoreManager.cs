using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text scoreText;
    public TMP_Text topscoreText;

    private int _score = 0;
    private int _topScore = 0;

    private const string TOPSCORE = "TOPSCORE";

    private void Awake()
    {
        instance = this;

        _topScore = PlayerPrefs.GetInt(TOPSCORE);
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
        PlayerPrefs.SetInt(TOPSCORE, _score);
    }

    private void UpdateUI()
    {
        scoreText.text = _score.ToString();
        topscoreText.text = _topScore.ToString();
    }
}

