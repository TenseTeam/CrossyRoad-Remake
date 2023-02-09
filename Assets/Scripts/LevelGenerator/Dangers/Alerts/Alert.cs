using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Alert : MonoBehaviour
{
    public ParticleSystem particles;
    public AudioSource audioAlert;

    public void StartAlert()
    {
        audioAlert.Play();
        particles.Play();
    }

}
