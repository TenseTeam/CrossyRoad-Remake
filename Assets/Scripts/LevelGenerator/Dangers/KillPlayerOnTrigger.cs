using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script that trigger the player's death.
/// </summary>
public class KillPlayerOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.PLAYER))
        {
            if(other.TryGetComponent<PlayerStatus>(out PlayerStatus ps))
            {
                ps.DeathByAccident();
            }
        }
    }
}
