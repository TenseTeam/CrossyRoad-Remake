using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Forward movement")]
    [Tooltip("Distance for every forward 'jump' movement")]
    public float Distance = 1;
    [Tooltip("Speed of the forward movement")]
    public float Speed;

    [Header("Right/left rotation")]
    [Tooltip("Speed of the rotation")]
    public float RotationSpeed;

    [Header("Raycast (to check obstacles)")]
    [Tooltip("range of the ray to check if there's something ahead of the player")] 
    public float rayRange = 1;
    [Tooltip("Layer of the non-killing obstacles")]
    public LayerMask ObstacleLayer;

    [Header("Audio")]
    public AudioSource source;

    // Update is called once per frame
    void Update()
    {
        //can move only if not obstacled by anything
        if (!IsObstacled())
        {
            //forward movement and rotation
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartCoroutine(RotateAndMove(new Vector3(transform.rotation.x, 0, transform.rotation.z)));
            }
            //left movement and rotation
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(RotateAndMove(new Vector3(transform.rotation.x, -90, transform.rotation.z)));
            }
            //right movement and rotation
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(RotateAndMove(new Vector3(transform.rotation.x, 90, transform.rotation.z)));
            }
            //backward movement and rotation
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
           
                StartCoroutine(RotateAndMove(new Vector3(transform.rotation.x, 180, transform.rotation.z)));
            }
        }
    }
    /// <summary>
    /// Rotation [first] and forward movement [last]
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    private IEnumerator RotateAndMove(Vector3 rotation)
    {
        float elapsedTime = 0;

        while (elapsedTime < RotationSpeed)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 1 - (elapsedTime / RotationSpeed));
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        source.Play();
        StartCoroutine(MoveForward());
        yield return null;
    }
    /// <summary>
    /// Forward movement (based of player transform)
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveForward()
    {
        Vector3 endPos = transform.position + (transform.forward * Distance);
        float elapsedTime = 0;

        StartCoroutine(Sound());
        while (elapsedTime < Speed)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, elapsedTime / Speed);
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
    /// <summary>
    /// set if the player is obstacled by something
    /// </summary>
    /// <returns></returns>
    private bool IsObstacled()
    {
        //debug to see where is the raycast by drawing it in scene
        Debug.DrawRay(transform.position, transform.forward * rayRange);
        //on raycast hit
        return Physics.Raycast(transform.position, transform.forward, rayRange, ObstacleLayer);
    }
    /// <summary>
    /// Plays the AudioSource and wait for it to end
    /// </summary>
    /// <returns></returns>
    private IEnumerator Sound()
    {
        source.Play();
        yield return new WaitWhile(() => source.isPlaying);
    }
}