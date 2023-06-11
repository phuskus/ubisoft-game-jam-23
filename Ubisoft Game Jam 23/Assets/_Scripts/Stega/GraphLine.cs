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
        Vector3 delta = p2 - p1;
        float buffer = rectTransform.rect.width + 50;
        rectTransform.position = p1 + delta.normalized * buffer;
        float lineLength = delta.magnitude - 2 * buffer;
        rectTransform.up = delta;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, lineLength);
    }
}
