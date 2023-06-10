using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockPassage : MonoBehaviour
{
    private bool open = false;

    public void SetOpenState(bool state)
    {
        if (open == state)
            return;
        open = state;
        float modifier = state ? -1 : 1;
        transform.DOMoveY(transform.position.y + modifier * 1.01f, 0.5f);
    }
}
