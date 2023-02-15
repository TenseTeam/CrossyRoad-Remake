using Extension.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerInRandomTransform : MonoBehaviour
{
    public GameObject prefab;
    [Range(0, 100)] public byte chance = 50;
    public Transform[] positions;
    public IntRange quantity;

    private void Start()
    {
        int counts = quantity.Random();

        if (counts > positions.Length)
            counts = positions.Length;

        for(int i = 0; i < counts; i++)
        {
            if (Random.Range(0, 101) > chance)
            {
                Instantiate(prefab, positions[Random.Range(0, positions.Length)].transform.position, Quaternion.identity)
                    .transform.SetParent(transform);
            }
        }
    }
}
