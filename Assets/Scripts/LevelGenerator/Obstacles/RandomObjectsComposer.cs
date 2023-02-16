using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;
using Extension.Types;

/// <summary>
/// Script for generating random objects by "slots" on a gameobject.
/// </summary>
public class RandomObjectsComposer : MonoBehaviour
{
    [Header("Origin")]
    public Transform spawnPoint;
    
    [Header("Obstacles")]
    public GameObject[] prefabs;
    [Range(0, 100)] public byte[] slots;
    [Range(1, 10)] public float spacing = 1;

    [Header("Special")]
    [Range(0, 100)] public byte chance = 50;
    public GameObject specialPrefab;
    public IntRange quantity;
    public IntRange specialSpawnRange;

    private List<int> _specialSlots;

    private void Start()
    {
        _specialSlots = new List<int>();

        int length = quantity.Random();

        if(length > specialSpawnRange.max - specialSpawnRange.min + 1)
        {
            Debug.Log("e");
            length = specialSpawnRange.max - specialSpawnRange.min + 1;
        }

        
        for (int i = 0; i < length; i++)
        {
            int randomSlot;

            do
            {
                randomSlot = specialSpawnRange.Random();
            } while (_specialSlots.Contains(randomSlot));

            _specialSlots.Add(randomSlot);
        }

        GenerateObstacles();
    }


    /// <summary>
    /// Generate the objects.
    /// </summary>
    void GenerateObstacles()
    {
        int effectiveQuantity = slots.Length;

        for (int k = 0; k < effectiveQuantity; k++)
        {
            Vector3 spawnPointPosition = spawnPoint.transform.position + (k * spacing * spawnPoint.transform.right);


            if (_specialSlots.Contains(k) && Random.Range(0, 101) <= chance)
            {
                GameObject obj = Instantiate(specialPrefab, spawnPointPosition, Quaternion.identity);
                obj.transform.SetParent(spawnPoint.transform);
            }
            else
            {
                if (Random.Range(0, 101) <= slots[k])
                {
                    GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPointPosition, Quaternion.identity);
                    obj.transform.SetParent(spawnPoint.transform);
                }
            }
        }
    }
}
