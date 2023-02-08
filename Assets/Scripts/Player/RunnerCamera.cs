using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCamera : MonoBehaviour
{
    public Transform target;
    [Range(1, 100)] public float speed = 10, recoverSpeed = 20;
    [Range(1, 100)] public float maxDistanceBeforeRecover = 30, deathDistance = 10;

    private void Update()
    {
        float distance = Mathf.Abs(transform.position.z - target.position.z);

#if DEBUG
        Debug.Log(distance);
#endif

        if (distance > deathDistance)
        {
            if (distance < maxDistanceBeforeRecover)
            {
                transform.position += Vector3.forward * speed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.forward * recoverSpeed * Time.deltaTime;
            }
        }
        else
        {
            // Add method for death
        }
    }
}
