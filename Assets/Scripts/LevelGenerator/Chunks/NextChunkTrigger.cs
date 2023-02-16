using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trigger used for generating a chunk
/// </summary>
public class NextChunkTrigger : MonoBehaviour
{
    public string triggerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            LevelGeneratorLoop.instance.GenerateChunk();
            this.enabled = false; // disabling the trigger for preventing the player to generate more chunks.
        }
    }

}
