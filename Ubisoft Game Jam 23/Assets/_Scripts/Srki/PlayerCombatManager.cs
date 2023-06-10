using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script manages health.
/// 
/// Child class of Damageable.
/// </summary>

[DisallowMultipleComponent]
//[RequireComponent(typeof(PlayerAction))]
//[RequireComponent(typeof(PlayerGunSelector))]
//[RequireComponent(typeof(SimpleCarMovement))]
//[RequireComponent(typeof(CharacterController))]
public class PlayerCombatManager : Damageable
{
    private bool deathTriggered;

    private void Start()
    {
        health = maxHealth;
    }

    public override void HandlePain()
    {
        // spawn some particles

        // play hit sound

        print("Put damage-player effects here!");
    }

    public override void HandleDeath()
    {
        if(deathTriggered) return;  // to prevent several calls

        deathTriggered = true;

        // activate explosion particles
        ParticleManager.Instance.SpawnPlayerDeathParticles(transform);

        // Deactivate movement


        // Call death event
        DeathEvent?.Invoke();

        // Deactivate player
        gameObject.SetActive(false);

        // Activate game lost UI
        
    }

}
