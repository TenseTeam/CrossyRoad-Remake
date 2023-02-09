using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabOnTrigger : MonoBehaviour
{
    public string triggerTag = "";

    internal bool hasGrabbed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            Debug.Log(other.transform.tag);

            hasGrabbed = true;

            if (other.transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }

            other.transform.parent = transform;
        }
    }
}
