using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;

    public float distance = 2f;


    private RectTransform _rect;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(right))
        {
            _rect.localPosition = new Vector3(_rect.localPosition.x - distance, _rect.localPosition.y, _rect.localPosition.z);
        }


        if (Input.GetKeyDown(left))
        {
            _rect.localPosition = new Vector3(_rect.localPosition.x + distance, _rect.localPosition.y, _rect.localPosition.z);
        }
    }



}
