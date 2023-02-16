using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public SO_Character[] characters;
    public ChaserCamera chaserCamera;

    private int selectedCharacter = 0;

    private void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt(Constants.SavePrefs.ID_SELECTED_CHARACTER);

        foreach(SO_Character character in characters)
        {
            if (character.id == selectedCharacter)
            {
                SpawnPlayer(character);
                break;
            }
        }
    }


    private void SpawnPlayer(SO_Character character)
    {
        chaserCamera.Target = Instantiate(character.prefab, transform.position, transform.rotation).transform;
    }
}
