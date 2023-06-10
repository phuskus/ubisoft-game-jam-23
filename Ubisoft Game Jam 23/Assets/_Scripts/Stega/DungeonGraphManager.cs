using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGraphManager : MonoBehaviour
{
    public static int CurrentLevel = 0;

    public static void HopLevels(int levelAmount)
    {
        CurrentLevel += levelAmount;
    }
}
