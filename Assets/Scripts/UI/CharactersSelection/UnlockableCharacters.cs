using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableCharacters : MonoBehaviour
{
    [Header("SO Characters")]
    public SO_Character[] characters;
    public uint idDefaultCharacter = 0;

    [Header("Costs")]
    public uint coinsCost = 100;

    [Header("Animation")]
    public Animator animatorPull;
    public string AnimationTriggerName = "Pull";

    void Awake() // Has to be in Awake so it checks if the characters are unlocked first before the CharacterSelector check if the character is unlocked
    {
        foreach(SO_Character character in characters)
        {
            if (character.id != idDefaultCharacter) // Dont load anything if the character is the default one
                character.isUnlocked = PlayerPrefs.GetInt(Constants.SavePrefs.CHARACTERS + character.id) == 0 ? false : true;
            else
                character.isUnlocked = true; // Making sure the default character is unlocked

#if DEBUG
            Debug.Log($"CHAR ID {character.id} IS UNLOCKED? {character.isUnlocked}");
#endif
        }
    }

    public void UnlockNewCharacter()
    {
        if (!isEveryCharacterUnlocked() // Just to making sure it's not going to loop
            && CoinsManager.instance.Deacrease((int)coinsCost)) 
        {
            SO_Character unlockedCharacter;
            
            do
            {
                unlockedCharacter = characters[Random.Range(0, characters.Length)];
            } while (unlockedCharacter.isUnlocked);

            unlockedCharacter.isUnlocked = true;

            PlayerPrefs.SetInt(Constants.SavePrefs.CHARACTERS + unlockedCharacter.id, 1);

            Debug.Log($"YOU HAVE UNLOCKED {unlockedCharacter.id}");
            animatorPull.SetTrigger(AnimationTriggerName);
        }
    }

    public bool isEveryCharacterUnlocked()
    {
        foreach (SO_Character character in characters)
        {
            if (!character.isUnlocked) return false;
        }

        return true;
    }
}
