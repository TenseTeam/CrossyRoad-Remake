using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrabOnTrigger : MonoBehaviour
{
    public string triggerTag = "";
    public bool HasGrabbed { get => _hasGrabbed; }

    private bool _hasGrabbed;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            _hasGrabbed = true;

            if (other.transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }

            other.transform.parent = transform;
        }
    }
}
