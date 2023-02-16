using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayerAudioManager : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip death;
    public AudioClip jump;

    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlayJump()
    {
        _audio.clip = jump;
        _audio.Play();
    }

    public void PlayDeath()
    {
        _audio.clip = death;
        _audio.Play();
    }
}
