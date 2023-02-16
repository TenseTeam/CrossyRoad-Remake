using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// CoinsManager Singleton used for managing the coins.
/// </summary>
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) // So when it changes scene it will find the new text reference.
    {
        if (Extension.Methods.Finder.TryFindGameObjectWithTag(Constants.Tags.UICOINS, out GameObject coinsUI))
        {
            _coinsText = coinsUI.GetComponent<TextMeshProUGUI>();
            UpdateUI();
        }
    }

    /// <summary>
    /// Increase by one the coins
    /// </summary>
    public void Increase()
    {
        _coins++;
        PlayerPrefs.SetInt(Constants.SavePrefs.COINS, _coins);
        UpdateUI();
    }

    /// <summary>
    /// Increase by X the coins
    /// </summary>
    /// <param name="quantityToAdd">X coins to add</param>
    public void Increase(int quantityToAdd)
    {
        quantityToAdd = Mathf.Abs(quantityToAdd);

        _coins+=quantityToAdd;
        PlayerPrefs.SetInt(Constants.SavePrefs.COINS, _coins);
        UpdateUI();
    }

    /// <summary>
    /// Decrase X coins
    /// </summary>
    /// <param name="quantityToRemove">X coins to remove</param>
    /// <returns></returns>
    public bool Decrease(int quantityToRemove)
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

    /// <summary>
    /// Update the UI with the coins.
    /// </summary>
    private void UpdateUI()
    {
        _coinsText.text = _coins.ToString();
    }
}
