using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudios : MonoBehaviour
{
    //Audio SFX
    [Header("Audio SFX")]
    [Tooltip("Frequency in seconds."), Range(1, 60)] public float audioFrequency = 1f;
    public AudioClip[] clips;

    private AudioSource _audio;
    private bool hasPlayedAudio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        PlayClip();
    }

    #region Audio

    private void PlayClip()
    {
        if (!hasPlayedAudio)
        {
            StartCoroutine(PlayAudio(clips[Random.Range(0, clips.Length)]));
        }
    }

    private IEnumerator PlayAudio(AudioClip clip)
    {
        _audio.clip = clip;
        _audio.Play();
        hasPlayedAudio = true;
        yield return new WaitForSeconds(audioFrequency);
        hasPlayedAudio = false;
    }

    #endregion
}
