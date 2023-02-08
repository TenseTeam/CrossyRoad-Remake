using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnTrigger : MonoBehaviour
{
    public string triggerTag = "Player";
    public uint toIncrease = 1;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            Extension.Methods.Finder.DestroyLastComponentOfType<Collider>(gameObject);
            ScoreManager.instance.Increase((int)toIncrease);
        }
    }
}
