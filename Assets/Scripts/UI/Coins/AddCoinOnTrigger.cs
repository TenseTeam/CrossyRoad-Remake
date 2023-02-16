using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple trigger to add a coin to the CoinsManager
/// </summary>
public class AddCoinOnTrigger : MonoBehaviour
{
    public string triggerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            CoinsManager.instance.Increase();
            Destroy(gameObject);
        }
    }
}
