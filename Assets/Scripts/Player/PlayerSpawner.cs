using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for spawning the chosen character.
/// </summary>
public class PlayerSpawner : MonoBehaviour
{
    public SO_Character[] characters;
    public ChaserCamera chaserCamera;

    private int selectedCharacter = 0;

    private void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt(Constants.SavePrefs.ID_SELECTED_CHARACTER); // Get the id of the seleceted character.

        foreach(SO_Character character in characters) // Find the one with the same ID
        {
            if (character.id == selectedCharacter)
            {
                SpawnPlayer(character); // Spawn it
                break;
            }
        }
    }

    /// <summary>
    /// Spawn a character based off its scriptable object
    /// </summary>
    /// <param name="character">Scriptable object of the character.</param>
    private void SpawnPlayer(SO_Character character)
    {
        chaserCamera.Target = Instantiate(character.prefab, transform.position, transform.rotation).transform;
    }
}
