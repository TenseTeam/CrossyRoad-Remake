using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (!character.isUnlocked)
        {
            _image.color = lockedColor;
        }
        else
        {
            _image.color = Color.white;
        }
    }

    public void Selected()
    {
        _anim.SetBool(OPEN_ANIM, true);
    }

    public void Deselected()
    {
        _anim.SetBool(OPEN_ANIM, false);
    }

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
