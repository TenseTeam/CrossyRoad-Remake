using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextChunkTrigger : MonoBehaviour
{
    public string triggerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            Extension.Methods.Finder.DestroyLastComponentOfType<Collider>(gameObject);
            LevelGeneratorLoop.instance.GenerateChunk();
        }
    }

}
