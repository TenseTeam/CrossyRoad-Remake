using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
