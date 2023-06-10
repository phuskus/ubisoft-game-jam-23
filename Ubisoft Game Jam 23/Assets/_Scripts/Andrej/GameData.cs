using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    #region Singleton
    public static GameData instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public PlayerData playerData;
    public CombatData combatData;
}
