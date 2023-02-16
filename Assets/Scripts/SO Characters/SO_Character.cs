using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SO of the character.
/// </summary>
[CreateAssetMenu(menuName = "Character")]
public class SO_Character : ScriptableObject
{
    public int id;
    public bool isUnlocked;
    public GameObject prefab;
}
