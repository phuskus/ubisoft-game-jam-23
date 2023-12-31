using System;
using UnityEngine;

public abstract class Damageable : MonoBehaviour, IHandleDeath
{
    [SerializeField] protected int maxHealth;

    protected int health;   // this needs to be defined in child classes

    public Action<int> TakeDamageEvent;
    public Action DeathEvent;

    public abstract void HandlePain();
    public abstract void HandleDeath();

    public void TakeDamage(int damage)
    {
        // Damage taken should always be 1 when the Player is the one doing the damage
        health -= damage;
        EliteHealthUpdate elite = GetComponent<EliteHealthUpdate>();
        if (elite) elite.DamageNextCube();
        TakeDamageEvent?.Invoke(damage);

        // react to damage
        ReactToDamage();
    }

    public void ReactToDamage()
    {

        // custom take-damage behaviour
        HandlePain();

        if (health <= 0)
        {
            // custom deal-with-death behaviour
            HandleDeath();
        }
    }
}
