using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Damageable : MonoBehaviour, IHandleDeath
{
    [SerializeField] protected float maxHealth;

    //[Space(10)]
    protected Slider healthBar;

    protected int health;   // this needs to be defined in child classes

    public Action<int> TakeDamageEvent;
    public Action DeathEvent;

    public abstract void HandlePain();
    public abstract void HandleDeath();

    public void TakeDamage(int Damage)
    {
        int damageTaken = Mathf.Clamp(Damage, 0, health);

        health -= damageTaken;
        healthBar.value = health;

        if (damageTaken != 0)
        {
            TakeDamageEvent?.Invoke(damageTaken);
        }

        // react to damage
        ReactToDamage();
    }

    public void ReactToDamage()
    {
        if (health != 0)
        {
            // custom take-damage behaviour
            HandlePain();
        }
        else
        {
            // custom deal-with-death behaviour
            HandleDeath();
        }
    }
}
