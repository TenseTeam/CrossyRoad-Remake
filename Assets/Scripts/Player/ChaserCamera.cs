using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ChaserCamera script that follow the player and kill the player
/// if the player stay out of view.
/// </summary>
public class ChaserCamera : MonoBehaviour
{
    [Header("Target")]
    public string targetTag = Constants.Tags.PLAYER;
    [Tooltip("Camere offset position by the X")]public float followTargetPositionXOffset = 5;

    [Header("Speeds")]
    [Range(1, 100)] public float lateralFollowSpeed = 10;
    [Tooltip("Default chase speed of the camera")] [Range(1, 100)] public float chaseSpeed = 10;
    [Tooltip("Speed of the camera that has to recover the distance between the player")] [Range(1, 100)] public float recoverSpeed = 20;

    [Header("Distances")]
    [Tooltip("Z max distance that triggers the recover speed")] [Range(1, 100)] public float recoverAt = 30;
    [Tooltip("Z min distance that triggers the out of view")] [Range(1, 100)] public float outOfViewAt = 10;

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
            ps.DeathByEnemy();
        }

        yield return null;
    }


    /// <summary>
    /// SideFollow the target
    /// </summary>
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

    /// <summary>
    /// Coroutine Camera lerp used to look and lock to a position until the grabber enemy grabbed something.
    /// </summary>
    /// <param name="speed">speed</param>
    /// <param name="offset">Z offset</param>
    /// <param name="grabber">EnemyGrabOnTrigger reference</param>
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