using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockPassage : MonoBehaviour
{
    private int _playerLayer;
    public bool Triggered { get; set; }
    private GridBlock gridBlock;    // The current segment

    private void Start()
    {
        _playerLayer = LayerMask.NameToLayer("Player");

        gridBlock = GetComponentInParent<GridBlock>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _playerLayer
            && gridBlock.BlockCleared // and segment is cleared
            )
        {
            Triggered = true;
        }
    }

    public void OpenPassage()
    {
        transform.DOMoveY(transform.position.y - 1.01f, 0.5f);
    }
}
