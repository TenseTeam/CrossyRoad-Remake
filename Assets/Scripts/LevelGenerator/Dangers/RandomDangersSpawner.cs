using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension.Types;

public class RandomDangersSpawner : MonoBehaviour
{
    public Transform spawnPointRight, spawnPointLeft;
    public GameObject[] dangers;
    public IntRange seriesOfDangers;
    public FloatRange speedRange;
    public float timeBeforeDestrucion;
    public float dangersSeriesSpacingTime;
    public float timeForNewSeries;

    private int _dangersToSpawn;
    private float _dangerSpeed;
    private Transform _spawnPoint;

    private void Start()
    {

        _spawnPoint = Random.Range(0, 1 + 1) == 0 ? spawnPointRight : spawnPointLeft;

        _dangersToSpawn = seriesOfDangers.Random();
        _dangerSpeed = speedRange.Random();

        StartCoroutine(SpawnLoop());
    }



    IEnumerator SpawnLoop()
    {
        for (int i = 0; i < _dangersToSpawn; i++)
        {
            GameObject danger = Instantiate(dangers[Random.Range(0, dangers.Length)], _spawnPoint.position, _spawnPoint.rotation);
            danger.transform.SetParent(transform);
            MoveForwardFor mf = danger.AddComponent<MoveForwardFor>();
            mf.speed = _dangerSpeed;
            mf.timeBeforeDestruction = timeBeforeDestrucion;

            yield return new WaitForSeconds(dangersSeriesSpacingTime);
        }

        yield return new WaitForSeconds(timeForNewSeries);
        StartCoroutine(SpawnLoop());
    }
}
