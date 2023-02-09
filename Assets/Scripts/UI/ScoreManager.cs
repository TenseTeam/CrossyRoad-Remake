using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private ushort _score = 0;
    public TMP_Text scoreText; 

    private void Awake()
    {
        instance = this;
        scoreText.text = _score.ToString();
    }

    public void Increase(ushort points)
    {
        _score += points;
        scoreText.text = _score.ToString();
    }
}

