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
    [Range(1, 100)] public float outOfViewAt = 10;

    private void Start()
    {
        StartCoroutine(MoveForward());
    }

    /// <summary>
    /// Camera Move Forward
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveForward()
    {
        float distance = Mathf.Abs(transform.position.z - target.position.z);

        while (distance > outOfViewAt)
        {
            distance = Mathf.Abs(transform.position.z - target.position.z);

            //Debug.Log(distance);


            if (distance < recoverAt)
                transform.position += Vector3.forward * speed * Time.fixedDeltaTime;
            else
                transform.position += Vector3.forward * recoverSpeed * Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        
        if(target.TryGetComponent(out PlayerStatus ps)) 
        {
            ps.DeathByEagle();
        }

        yield return null;
    }


    public void CameraDeath(float speed, float offset, GrabOnTrigger grabber) 
    {
        StartCoroutine(LerpCamera(speed, offset, grabber));
    }

    public void CameraDeath(float speed, float offset)
    {
        StartCoroutine(LerpCamera(speed, offset));
    }

    private IEnumerator LerpCamera(float speed, float offset, GrabOnTrigger grabber)
    {
        while (!grabber.HasGrabbed)
        {
            float lerpZ = Mathf.Lerp(transform.position.z, target.position.z - offset, speed * Time.fixedDeltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, lerpZ);

            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    private IEnumerator LerpCamera(float speed, float offset)
    {
        while (true)
        {
            float lerpZ = Mathf.Lerp(transform.position.z, target.position.z - offset, speed * Time.fixedDeltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, lerpZ);

            yield return new WaitForFixedUpdate();
        }
    }
}
