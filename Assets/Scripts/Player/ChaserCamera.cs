using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserCamera : MonoBehaviour
{
    [Header("Target")]
    public string targetTag = Constants.Tags.PLAYER;
    public float followTargetPositionXOffset = 5;

    [Header("Speeds")]
    [Range(1, 100)] public float lateralFollowSpeed = 10;
    [Range(1, 100)] public float chaseSpeed = 10;
    [Range(1, 100)] public float recoverSpeed = 20;

    [Header("Distances")]
    [Range(1, 100)] public float recoverAt = 30;
    [Range(1, 100)] public float outOfViewAt = 10;

    [Header("UI")]
    public GameObject menuUI;
    public GameObject gameTitle;

    private bool started = false;
    private Transform _target;

    public Transform Target { get => _target; set => _target = value; }

    private void Update()
    {
        if (!started && (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)) 
        {
            menuUI.SetActive(false);
            gameTitle.SetActive(false);
            StartCoroutine(CameraMovement());
            started = true;
        }
    }

    /// <summary>
    /// Camera Move Forward
    /// </summary>
    /// <returns></returns>
    private IEnumerator CameraMovement()
    {
        float distance = Mathf.Abs(transform.position.z - _target.position.z);

        while (distance > outOfViewAt)
        {
            distance = Mathf.Abs(transform.position.z - _target.position.z);

#if DEBUG
            Debug.Log(distance);
#endif

            SideFollow();

            if (distance < recoverAt)
                transform.position += Vector3.forward * chaseSpeed * Time.fixedDeltaTime;
            else
                transform.position += Vector3.forward * recoverSpeed * Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        
        if(_target.TryGetComponent(out PlayerStatus ps)) 
        {
            ps.DeathByEagle();
        }

        yield return null;
    }

    private void SideFollow()
    {
        float lerpX = Mathf.Lerp(transform.position.x + followTargetPositionXOffset, _target.position.x, lateralFollowSpeed * Time.fixedDeltaTime);
        transform.position = new Vector3(lerpX, transform.position.y, transform.position.z);
    }

    public void StartCameraLerp(float speed, float offset, EnemyGrabOnTrigger grabber) 
    {
        StopAllCoroutines();
        StartCoroutine(CameraLerp(speed, offset, grabber));
    }

    public void StartCameraLerp(float speed, float offset)
    {
        StopAllCoroutines();
        StartCoroutine(CameraLerp(speed, offset));
    }


    private IEnumerator CameraLerp(float speed, float offset, EnemyGrabOnTrigger grabber)
    {
        while (!grabber.HasGrabbed)
        {
            float lerpZ = Mathf.Lerp(transform.position.z, _target.position.z - offset, speed * Time.fixedDeltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, lerpZ);

            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    private IEnumerator CameraLerp(float speed, float offset)
    {
        while (true)
        {
            float lerpZ = Mathf.Lerp(transform.position.z, _target.position.z - offset, speed * Time.fixedDeltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, lerpZ);

            yield return new WaitForFixedUpdate();
        }
    }



}