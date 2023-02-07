using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPreviousChunkTrigger : MonoBehaviour
{
    public string triggerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            Collider[] colliders = GetComponents<Collider>();
            Collider lastCollider = colliders[colliders.Length - 1];

            Destroy(lastCollider);

            LevelGeneratorLoop.instance.DestroyLastChunk();
        }
    }
}
