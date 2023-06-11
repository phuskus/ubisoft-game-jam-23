using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script manages health.
/// 
/// Child class of Damageable.
/// </summary>

[DisallowMultipleComponent]
public class PlayerCombatManager : Damageable
{
    private void Start()
    {
        health = maxHealth;
    }

    public override void HandlePain()
    {
        GameFlowManager.I.ReduceTime();
    }

    public override void HandleDeath()
    {
        // activate explosion particles
        ParticleManager.Instance?.SpawnPlayerDeathParticles(transform);

        // Deactivate movement
        
        // Call death event
        DeathEvent?.Invoke();

        // Activate game lost UI
        
    }
}
