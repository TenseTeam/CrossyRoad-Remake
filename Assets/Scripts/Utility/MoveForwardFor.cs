using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardFor : MonoBehaviour
{
    public float speed;
    public float duration;

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
