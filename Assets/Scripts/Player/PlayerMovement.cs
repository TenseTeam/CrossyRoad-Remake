using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerAnimatorController), typeof(PlayerAudioManager))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Distance for vertical movement")]
    public float verticalDistance = 2f;
    [Tooltip("Distance for lateral movement")]
    public float lateralDistance = 1f;

    [Header("Speed")]
    public float speed;

    private PlayerAnimatorController _anim;
    private PlayerAudioManager _playerAudio;

    private Vector3 _destinationPosition;
    private bool _isMoving = false;

    [Header("Raycast (to check obstacles)")]
    [Tooltip("range of the ray to check if there's something ahead of the player")]
    public float rayRange = 1;
    public float verticalOffsetOrigin = 1f;
    [Tooltip("Layer of the non-killing obstacles")]
    public LayerMask ObstacleLayer;

    private void Start()
    {
        _anim = GetComponent<PlayerAnimatorController>();
        _playerAudio = GetComponent<PlayerAudioManager>();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RotateAndMove(0f, verticalDistance));
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(RotateAndMove(-90f, lateralDistance));
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(RotateAndMove(90f, lateralDistance));
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(RotateAndMove(180f, verticalDistance));
        }

#if DEBUG
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + verticalOffsetOrigin, transform.position.z), transform.forward * rayRange);
#endif
    }


    private IEnumerator RotateAndMove(float angle, float distance)
    {
        if (!_isMoving)
        {
            _isMoving = true;
            _playerAudio.PlayJump();

            _anim.Jump();
            Quaternion rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);

            if (transform.rotation != rotation)
            {
                Debug.Log(transform.rotation != rotation);
                StartCoroutine(Rotate(rotation));
            }

            if (!IsObstacled())
            {
                _destinationPosition = transform.position + (transform.forward * distance);
                for (float elapsedTime = 0f; elapsedTime < speed; elapsedTime += Time.deltaTime)
                {
                    transform.position = Vector3.Lerp(transform.position, _destinationPosition, elapsedTime / speed);
                    yield return new WaitForEndOfFrame();
                }
            }
            
            _isMoving = false;
            yield return null;
        }

    }

    private IEnumerator Rotate(Quaternion rotation)
    {
        for (float elapsedTime = 0f; elapsedTime < speed; elapsedTime += Time.deltaTime)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1 - (elapsedTime / speed));
            yield return new WaitForEndOfFrame();
        }
    }

    private bool IsObstacled()
    {
        //on raycast hit
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + verticalOffsetOrigin, transform.position.z);
        return Physics.Raycast(origin, transform.forward, rayRange, ObstacleLayer);
    }
}
