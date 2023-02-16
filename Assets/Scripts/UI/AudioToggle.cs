using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AudioToggle : MonoBehaviour
{
    public Sprite on, off;

    private Image _image;
    private bool IsOff { get; set; } = false;

    private void Start()
    {
        _image = GetComponent<Image>();

        IsOff = PlayerPrefs.GetInt(Constants.SavePrefs.AUDIO_ENABLED, 1) == 1 ? false : true;
        Check();
    }

    public void Check()
    {
        if (IsOff) Off();
        else On();
    }

    public void Toggle()
    {
        if (IsOff) On();
        else Off();
    }

    public void On()
    {
        IsOff = false;
        AudioListener.pause = false;
        _image.sprite = on;
        PlayerPrefs.SetInt(Constants.SavePrefs.AUDIO_ENABLED, 1);
    }

    public void Off()
    {
        IsOff = true;
        AudioListener.pause = true;
        _image.sprite = off;
        PlayerPrefs.SetInt(Constants.SavePrefs.AUDIO_ENABLED, 0);
    }
}
