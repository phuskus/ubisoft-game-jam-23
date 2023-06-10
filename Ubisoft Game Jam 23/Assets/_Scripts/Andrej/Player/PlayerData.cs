using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "GameData/PlayerData", order = 51)]
public class PlayerData : SingletonSO<PlayerData>
{
    [Header(">>> Player Stats")]
    public float playerStamina;

    public float playerMaxStamina = 2f;
    public float staminaDepletionSpeed = 1f;

    public float playerWalkSpeed = 10f;
    public float playerRunSpeed = 15f;

    public float TurnRate = 20f;
    public float Acceleration = 1f;

    [Header(">>> Camera Control")]

    public float cameraExtendRadius = 10f;
    public float CameraHeadDistance = 10f;
    public float CameraFollowSpeed = 15f;
}
