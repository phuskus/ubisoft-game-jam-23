using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomExitBlock : MonoBehaviour
{
    public void ExitDungeon()
    {
        DungeonGraphManager.OnDungeonCompleted();
    }
}
