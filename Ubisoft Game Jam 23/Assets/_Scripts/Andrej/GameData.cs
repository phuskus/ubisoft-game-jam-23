using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class GameData : SingletonMono<GameData>
{
    public static PlayerData PlayerData;
    public static CombatData CombatData;

    private void Start()
    {
        PlayerData = PlayerData.Instance;
        CombatData = CombatData.Instance;
    }
}
