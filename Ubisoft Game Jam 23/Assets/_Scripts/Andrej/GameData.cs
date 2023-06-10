using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class GameData : SingletonMono<GameData>
{
    public static CombatData CombatData;

    private void Start()
    {
        CombatData = CombatData.Instance;
    }
}
