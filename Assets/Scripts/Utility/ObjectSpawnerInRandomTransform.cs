using Extension.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerInRandomTransform : MonoBehaviour
{
    public GameObject prefab;
    [Range(0, 100)] public byte chance = 50;
    public List<Transform> positions;
    public IntRange quantity;

    private void Start()
    {
        int counts = quantity.Random();

        if (counts > positions.Count)
            counts = positions.Count;

        for(int i = 0; i < counts; i++)
        {
            if (chance > Random.Range(0, 100 + 1))
            {
                int indexPos = Random.Range(0, positions.Count);

                Instantiate(prefab, positions[indexPos].transform.position, Quaternion.identity)
                    .transform.SetParent(transform);

                positions.RemoveAt(indexPos);
            }
        }
    }
}
