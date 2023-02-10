using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _StartPos;
    private Vector3 _EndPos;
    private float _ElapsedTime;
    private Quaternion _StartRotation;
    private Quaternion _EndRotation;

    [Header("Forward movement")]
    [Tooltip("Distance for every forward 'jump' movement")]
    [SerializeField] public float Distance = 1;
    [Tooltip("Speed of the forward movement")]
    [SerializeField] public float Speed;
    [Header("Right/left rotation")]
    [Tooltip("Speed of the rotation")]
    [SerializeField] public float RotationSpeed;
    [Tooltip("Angle of the rotation in Y axis")]
    [SerializeField] public float RotationAngle;
    private Vector3 _rotation;

    [Header("Raycast (to check obstacles)")]
    [Tooltip("range of the ray to check if there's something ahead of the player")]
    [SerializeField] public float rayRange = 1;
    [Tooltip("Tag of the non-killing obstacles")]
    [SerializeField] public string ObstacleTag;
    private bool _obstacled = false;

    private void Start()
    {
        _rotation = new Vector3(0, RotationAngle, 0);
    }
    // Update is called once per frame
    void Update()
    {
        //direction of the raycast
        Vector3 rayDirection = Vector3.forward;
        //effective raycast
        Ray ray = new Ray(transform.position, transform.TransformDirection(rayDirection * rayRange));
        //debug to see where is the raycast by drawing it in scene
        Debug.DrawRay(transform.position, transform.TransformDirection(rayDirection * rayRange));
        //on raycast hit
        if (Physics.Raycast(ray, out RaycastHit hit, rayRange))
        {
            if (hit.collider.CompareTag("ObstacleTag"))
                _obstacled = true;
            else _obstacled = false;
        }
        else _obstacled = false;

        if (_obstacled == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartCoroutine(ForwardMove(Speed));
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _EndRotation = Quaternion.Euler(-_rotation);
            StartCoroutine(RotationMove(RotationSpeed));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _EndRotation = Quaternion.Euler(_rotation);
            StartCoroutine(RotationMove(RotationSpeed));
        }

    }
    private IEnumerator ForwardMove(float time)
    {
        _StartPos = transform.position;
        _EndPos = transform.position + (transform.forward * Distance);
        _ElapsedTime = 0;

        while (_ElapsedTime < time)
        {
            transform.position = Vector3.Lerp(_StartPos, _EndPos, (_ElapsedTime / time));
            _ElapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator RotationMove(float time)
    {
        _StartRotation = transform.rotation;
        _ElapsedTime = 0;

        while (_ElapsedTime < time)
        {
            transform.rotation = Quaternion.Lerp(_StartRotation, _EndRotation, 1 - (_ElapsedTime / time));
            _ElapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}