using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combat Data", menuName = "GameData/CombatData")]
public class CombatData : ScriptableObject
{
    [Space(15f), Header(">>> Monster Damage Resistance")]

    public float mickoDamageSpeed = 0.4f;
    public float wendigoDamageSpeed = 0.6f;
    public float bobDamageSpeed = 0.8f;
    public float mileDamageSpeed = 0.8f;

    [Space(15f)] public float killThreshold = 0.75f;

    [Space(15f), Header(">>> Monster Normal Speed")]

    public float mickoSpeed = 5f;
    public float wendigoSpeed = 7.5f;
    public float bobSpeed = 10f;
    public float mileSpeed = 10f;

    [Space(15f), Header(">>> Monster Slow Speed")]

    public float mickoSlowSpeed = 2.5f;
    public float wendigoSlowSpeed = 3.75f;
    public float bobSlowSpeed = 5f;
    public float mileSlowSpeed = 5f;

    [Space(15f), Header(">>> Monster Attack Damage")]

    public int mickoAttack = 1;
    public int wendigoAttack = 2;
    public int bobAttack = 4;
    public int mileAttack = 4;

    [Space(15f), Header(">>> Monster Attack Start Range")]

    public float mickoStartRange = 5f;
    public float wendigoStartRange = 5f;
    public float bobStartRange = 5f;
    public float mileStartRange = 5f;

    [Space(15f), Header(">>> Monster Attack Catch Range")]

    public float mickoCatchRange = 10f;
    public float wendigoCatchRange = 10f;
    public float wendigoBombDelay = 2f;
    public float bobCatchRange = 10f;
    public float mileCatchRange = 10f;

    [Space(15f), Header(">>> Monster Attack Catch Range")]

    public float agroRange = 10f;
}
