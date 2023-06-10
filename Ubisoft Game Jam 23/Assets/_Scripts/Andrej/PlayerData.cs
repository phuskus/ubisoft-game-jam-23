using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "GameData/PlayerData", order = 51)]
public class PlayerData : SingletonSO<PlayerData>
{
    [Space(10f), Header(">>> Player Stats")]

    [Space(10f)] public int playerHealth = 5;
    public float playerStamina;
    public float playerBattery;

    [Space(10f)] public float playerMaxStamina = 2f;
    public float staminaDepletionSpeed = 1f;

    [Space(10f)] public float playerWalkSpeed = 10f;
    public float playerRunSpeed = 15f;

    [Space(10f)] public float moveSmoothFactor = 5f;

    [Space(10f)] public float playerMaxBattery = 2f;
    public float batteryDepletionSpeed = 0.01f;

    [Space(10f), Header(">>> Camera Control")]

    public float cameraExtendRadius = 10f;
    public float minimapZoom = 15f;

    [Space(10f), Header(">>> Ghost Stats")]

    public float ghostSpeed = 10f;
}
