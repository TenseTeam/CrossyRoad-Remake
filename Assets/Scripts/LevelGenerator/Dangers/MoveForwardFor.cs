using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardFor : MonoBehaviour
{
    public float timeBeforeDestruction;
    public float speed;


    private void Start()
    {
        Destroy(gameObject, timeBeforeDestruction);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
