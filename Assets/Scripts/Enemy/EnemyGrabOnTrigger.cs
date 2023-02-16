using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script is used for the enemy that grabs the player.
/// </summary>
public class EnemyGrabOnTrigger : MonoBehaviour
{
    public string triggerTag = "";
    public bool HasGrabbed { get; set; }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            HasGrabbed = true;

            if (other.transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }

            other.transform.parent = transform;
        }
    }
}
