using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager instance;
    private int _coins;
    private TMP_Text _coinsText;

    public int CurrentCoins { get => _coins; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            _coins = PlayerPrefs.GetInt(Constants.SavePrefs.COINS);
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Extension.Methods.Finder.TryFindGameObjectWithTag(Constants.Tags.UICOINS, out GameObject coinsUI))
        {
            _coinsText = coinsUI.GetComponent<TextMeshProUGUI>();

            UpdateUI();
        }
    }

    public void Increase()
    {
        _coins++;
        PlayerPrefs.SetInt(Constants.SavePrefs.COINS, _coins);
        UpdateUI();
    }

    public void Increase(int quantityToAdd)
    {
        quantityToAdd = Mathf.Abs(quantityToAdd);

        _coins+=quantityToAdd;
        PlayerPrefs.SetInt(Constants.SavePrefs.COINS, _coins);
        UpdateUI();
    }


    public bool Deacrease(int quantityToRemove)
    {
        quantityToRemove = Mathf.Abs(quantityToRemove);

        if (_coins - quantityToRemove >= 0)
        {
            _coins -= quantityToRemove;
            PlayerPrefs.SetInt(Constants.SavePrefs.COINS, _coins);
            UpdateUI();

            return true;
        }

        return false;
    }


    private void UpdateUI()
    {
        _coinsText.text = _coins.ToString();
    }
}
