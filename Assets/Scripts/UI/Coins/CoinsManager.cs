using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager instance;
    public TMP_Text coinsText;
    private const string COINS = "COINS";
    private int _coins;


    private void Awake()
    {
        instance = this;
        _coins = PlayerPrefs.GetInt(COINS);
        UpdateUI();   
    }


    public void Increase()
    {
        _coins++;
        PlayerPrefs.SetInt(COINS, _coins);
        UpdateUI();
    }


    private void UpdateUI()
    {
        coinsText.text = _coins.ToString();
    }
}
