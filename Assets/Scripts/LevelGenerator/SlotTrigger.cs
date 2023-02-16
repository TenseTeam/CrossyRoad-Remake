using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotTrigger : MonoBehaviour
{
    public string triggerTag = Constants.Tags.PLAYER;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            other.transform.parent = transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            other.transform.parent = null;
        }
    }
}
