using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character")]
public class SO_Character : ScriptableObject
{
    public int id;
    public bool isUnlocked;
    public GameObject prefab;
}
