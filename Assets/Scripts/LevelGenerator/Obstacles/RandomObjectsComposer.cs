using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class RandomObjectsComposer : MonoBehaviour
{
    [Header("Origin")]
    public Transform spawnPoint;

    [Header("Obstacles")]
    public GameObject[] prefabs;
    [Range(0, 100)] public byte[] slots;
    [Range(1, 10)] public float spacing = 1;


    private void Start()
    {
        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        int effectiveQuantity = slots.Length;

        for (int k = 0; k < effectiveQuantity; k++)
        {
            Vector3 spawnPointPosition = spawnPoint.transform.position + (k * spacing * spawnPoint.transform.right);

            if (Random.Range(0, 101) <= slots[k])
            {
                GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPointPosition, Quaternion.identity);
                obj.transform.SetParent(spawnPoint.transform);
            }
        }
    }
}
