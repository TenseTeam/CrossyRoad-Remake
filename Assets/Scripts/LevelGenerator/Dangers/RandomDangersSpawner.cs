using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension.Types;

public class RandomDangersSpawner : MonoBehaviour
{
    [Header("Origins")]
    public Transform[] spawnPoints;

    [Header("Dangers")]
    public GameObject[] dangers;

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

        StartCoroutine(SpawnLoop());
    }



    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(_timeForNewSeries);

        for (int i = 0; i < _dangersToSpawn; i++)
        {
            GameObject danger = Instantiate(dangers[Random.Range(0, dangers.Length)], _spawnPoint.position, _spawnPoint.rotation);
            danger.transform.SetParent(transform);
            MoveForwardFor mf = danger.AddComponent<MoveForwardFor>();
            mf.speed = _dangerSpeed;
            mf.duration = this.duration;

            yield return new WaitForSeconds(spacing);
        }
        
        StartCoroutine(SpawnLoop());
    }
}
