using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserCamera : MonoBehaviour
{
    public Transform target;

    [Header("Speeds")]
    [Range(1, 100)] public float speed = 10;
    [Range(1, 100)] public float recoverSpeed = 20;

    [Header("Distances")]
    [Range(1, 100)] public float recoverAt = 30;
    [Range(1, 100)] public float deathAt = 10;

    [Header("Death")]
    public GameObject grabber;

    private bool _isDead = false;

    private void Update()
    {
        if (!_isDead)
            MoveForward();
        else
            Death();
    }


    private void MoveForward()
    {
        float distance = Mathf.Abs(transform.position.z - target.position.z);

        if (distance > deathAt)
        {
            _isDead = true;
            return;
        }

        if (distance < recoverAt)
            transform.position += Vector3.forward * speed * Time.deltaTime;
        else
            transform.position += Vector3.forward * recoverSpeed * Time.deltaTime;
    }

    private void Death()
    {
        grabber.SetActive(true);

    }
}
