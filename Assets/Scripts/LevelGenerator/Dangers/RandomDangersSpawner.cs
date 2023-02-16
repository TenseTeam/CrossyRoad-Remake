using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension.Types;

/// <summary>
/// Script for generating random object from various spawnpoints
/// and adding a MoveForwardFor script to each of the gameobject instantiated.
/// </summary>
public class RandomDangersSpawner : MonoBehaviour
{
    [Header("Origins")]
    public Transform[] spawnPoints;

    [Header("Dangers")]
    public GameObject[] dangers;
    [Range(1, 60)]public float forewarningTime = 1f;
    public bool prewarm = false;
    public float spawnOffsetPrewarm = 10f;

    [Header("Series")]
    public IntRange series;
    public FloatRange speed;
    public IntRange spawnRate;
    public float spacing;

    [Header("Dispose")]
    [Range(1, 60)] public float duration = 30;

    private int _timeForNewSeries;
    private int _dangersToSpawn;
    private float _dangerSpeed;
    private Transform _spawnPoint;


    private void Start()
    {
        _timeForNewSeries = spawnRate.Random();
        _spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        _dangersToSpawn = series.Random();
        _dangerSpeed = speed.Random();


        if (prewarm)
        {
            Vector3 originPrewarm = new Vector3(_spawnPoint.position.x, _spawnPoint.position.y, _spawnPoint.position.z + spawnOffsetPrewarm);
            SpawnDanger(originPrewarm, _spawnPoint.rotation);
        }

        StartCoroutine(SpawnLoop());
    }


    /// <summary>
    /// Spawn Loop Coroutine to keep instantiating the objects.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(forewarningTime);
        
        SendMessage("StartAlert", SendMessageOptions.DontRequireReceiver);

        yield return new WaitForSeconds(_timeForNewSeries - forewarningTime);

        for (int i = 0; i < _dangersToSpawn; i++)
        {
            SpawnDanger(_spawnPoint.position, _spawnPoint.rotation);
            yield return new WaitForSeconds(spacing);
        }
        
        StartCoroutine(SpawnLoop());
    }


    /// <summary>
    /// Method that effectively instantiate the object.
    /// </summary>
    /// <param name="position">position of instantiate</param>
    /// <param name="rotation">rotation of instantiate</param>
    private void SpawnDanger(Vector3 position, Quaternion rotation)
    {
        GameObject danger = Instantiate(dangers[Random.Range(0, dangers.Length)], position, rotation);
        danger.transform.SetParent(transform);
        MoveForwardFor mf = danger.AddComponent<MoveForwardFor>();
        mf.speed = _dangerSpeed;
        mf.duration = this.duration;
    }
}
