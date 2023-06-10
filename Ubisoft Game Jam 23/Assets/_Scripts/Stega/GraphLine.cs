using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphLine : MonoBehaviour
{
    private RectTransform rectTransform;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetPoints(Vector3 p1, Vector3 p2)
    {
        rectTransform.position = p1;
        rectTransform.up = (p2 - p1);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (p2 - p1).magnitude);
    }
}
