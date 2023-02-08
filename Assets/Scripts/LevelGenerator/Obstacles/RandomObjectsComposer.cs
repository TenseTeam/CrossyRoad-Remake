using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class RandomObjectsComposer : MonoBehaviour
{
    public GameObject[] objects;
    [Range(0, 100)]public uint chanceOfObstacle = 50;
    public uint minimumFreeSlots = 1;
    public uint confinementSlots = 3;
    [Range(1, 10)]public float spawnPointSpacing = 1;
    public Vector3 originOffset;

    private float _scaleX;
    private int _numberOfSpawnPoints;

    private void Start()
    {
        _scaleX = GetComponent<Renderer>().bounds.size.x;
        _numberOfSpawnPoints = Mathf.FloorToInt(_scaleX / spawnPointSpacing);

        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        int spawned = (int)minimumFreeSlots;

        for (int i = 0; i < _numberOfSpawnPoints; i++)
        {
            Vector3 spawnPointPosition = (transform.position + originOffset) + (i * spawnPointSpacing * Vector3.right);

            if ( (spawned < _numberOfSpawnPoints
                && Extension.Methods.Mathematics.Chance(chanceOfObstacle, 100) )
                || (i < confinementSlots || i > _numberOfSpawnPoints - confinementSlots - 1))
            {
                GameObject obj = Instantiate(objects[Random.Range(0, objects.Length)], spawnPointPosition, Quaternion.identity);
                obj.transform.SetParent(transform);
                spawned++;
            }
        }
    }
}
