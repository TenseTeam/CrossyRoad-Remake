using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trigger used for destroying the previous chunk
/// </summary>
public class DestroyPreviousChunkTrigger : MonoBehaviour
{
    public string triggerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            LevelGeneratorLoop.instance.DestroyLastChunk();
            this.enabled = false;
        }
    }
}
