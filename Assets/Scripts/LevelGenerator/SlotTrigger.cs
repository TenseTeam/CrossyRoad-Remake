using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotTrigger : MonoBehaviour
{
    public string triggerTag = Constants.Tags.PLAYER;
    public float drag = 5f;
    public float centerPrecision = 0.2f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            other.transform.parent = transform.parent;
            StartCoroutine(CenterInSlot(other.transform));
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            other.transform.parent = null;
        }
    }

    private IEnumerator CenterInSlot(Transform toThrow)
    {
        while (
            !Extension.Methods.Mathematics.Approximately(toThrow.transform.position.x, transform.position.x, centerPrecision)
            || !Extension.Methods.Mathematics.Approximately(toThrow.transform.position.z, transform.position.z, centerPrecision)) //position check
        {

            toThrow.transform.position = Vector3.Lerp(toThrow.transform.position,
                     new Vector3(transform.position.x, toThrow.transform.position.y, transform.position.z), drag * Time.fixedDeltaTime); //Set position based only on the Y

            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }
}
