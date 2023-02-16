using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Character selector script
/// </summary>
[RequireComponent(typeof(Animator), typeof(RawImage))]
public class CharacterSelector : MonoBehaviour
{
    private const string OPEN_ANIM = "Zoomed";

    public SO_Character character;
    public Color lockedColor = Color.black;

    public CameraFade fadeCam;
    public SwitchScene sceneSwitcher;

    private Animator _anim;
    private RawImage _image;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _image = GetComponent<RawImage>();
    }

    private void OnEnable()
    {
        if (!character.isUnlocked) // Check if the character is unlocked
        {
            _image.color = lockedColor; // if not change the color
        }
        else
        {
            _image.color = Color.white; // otherwise set white to make it visible
        }
    }

    /// <summary>
    /// Button selected acition for the animator
    /// </summary>
    public void Selected()
    {
        _anim.SetBool(OPEN_ANIM, true);
    }

    /// <summary>
    /// Button deselected acition for the animator
    /// </summary>
    public void Deselected()
    {
        _anim.SetBool(OPEN_ANIM, false);
    }

    /// <summary>
    /// Submit the character you want to use and change scene
    /// </summary>
    public void SubmitCharacter()
    {
        if (character.isUnlocked)
        {
            PlayerPrefs.SetInt(Constants.SavePrefs.ID_SELECTED_CHARACTER, character.id);
            fadeCam.DoFadeIn(fadeCam.fadeDuration);
            sceneSwitcher.ChangeScene();
        }
    }
}
