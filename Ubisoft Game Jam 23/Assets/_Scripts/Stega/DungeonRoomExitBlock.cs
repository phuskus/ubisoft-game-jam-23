using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomExitBlock : MonoBehaviour
{
    [SerializeField] private GameObject notClearedUI;
    
    private void OnTriggerEnter(Collider other)
    {
        if (DungeonLevelManager.RoomsLeftToClear == 0 && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            DungeonGraphManager.OnDungeonCompleted();
        }
    }

    private void Update()
    {
        notClearedUI.SetActive(DungeonLevelManager.RoomsLeftToClear > 0);
    }
}
