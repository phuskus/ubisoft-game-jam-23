using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class DungeonLevelManager : MonoBehaviour
{
    public static DungeonLevelManager I { get; private set; }

    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        DungeonGraphManager.OnDungeonCompleted();
    }
}
