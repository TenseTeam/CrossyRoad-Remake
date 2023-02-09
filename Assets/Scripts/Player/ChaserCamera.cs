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
    public GameObject restartUI;
    public float speedCentering;
    public float offsetForCentering;

    private SimpleMove _movementPlayer;
    private GrabOnTrigger _grab;

    private void Awake()
    {
        //Change SimpleMove with the RealMovement of the Player or disable his controls
        if (target.TryGetComponent<SimpleMove>(out SimpleMove mov))
        {
            _movementPlayer = mov;
        }

        if (grabber.TryGetComponent<GrabOnTrigger>(out GrabOnTrigger grab))
        {
            _grab = grab;
        }
    }


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

        while (distance > deathAt)
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


    public void CameraDeath() 
    {
        StartCoroutine(LerpCamera());
    }

    private IEnumerator LerpCamera()
    {
        while (!_grab.hasGrabbed)
        {
            float lerpZ = Mathf.Lerp(transform.position.z, target.position.z - offsetForCentering, speedCentering * Time.fixedDeltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, lerpZ);

            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }
}
