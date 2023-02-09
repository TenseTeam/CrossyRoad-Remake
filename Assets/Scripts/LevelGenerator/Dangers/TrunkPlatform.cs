using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkPlatform : MonoBehaviour
{
    public string collTag = Constants.Tags.PLAYER;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(collTag))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag(collTag))
        {
            collision.transform.SetParent(null);
        }
    }
}
