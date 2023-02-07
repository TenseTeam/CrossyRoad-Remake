using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class RandomObjectsComposer : MonoBehaviour
{
    public GameObject[] objects;
    [Range(0, 100)]public uint chanceOfObstacle = 50;
    public uint minimumFreeSlots = 1;
    
    [SerializeField] private Transform[] _confinementSlots;
    [SerializeField] private Transform[] _objstacleSlots;


    private void Start()
    {
        GenerateConfinement();
        GenerateObstacles();
    }


    void GenerateConfinement()
    {
        foreach (Transform slot in _confinementSlots)
        {
            GameObject obj = Instantiate(objects[Random.Range(0, objects.Length)], slot.position, slot.rotation);
            obj.transform.SetParent(slot.transform);
        }
    }

    void GenerateObstacles()
    {
        int spawned = (int)minimumFreeSlots;

        foreach (Transform slot in _objstacleSlots)
        { 
            if (spawned < _objstacleSlots.Length 
                && Extension.Methods.Mathematics.Chance(chanceOfObstacle, 100))
            {
                GameObject obj = Instantiate(objects[Random.Range(0, objects.Length)], slot.position, slot.rotation);
                obj.transform.SetParent(slot.transform);
                spawned++;
            }
        }
    }
}
