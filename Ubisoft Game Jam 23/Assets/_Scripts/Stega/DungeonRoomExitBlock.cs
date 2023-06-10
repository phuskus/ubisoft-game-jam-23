using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomExitBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            DungeonGraphManager.OnDungeonCompleted();
        }
    }
}
