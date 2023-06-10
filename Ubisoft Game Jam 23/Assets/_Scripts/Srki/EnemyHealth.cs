using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Child class of Damageable
/// </summary>

[DisallowMultipleComponent]
public class EnemyHealth : Damageable
{
    public int Health { get => health; set => health = value; } // property of inherited field

    [SerializeField] private EnemyMovement Movement;

    private Enemy enemy;

    private void Start()
    {
        Movement = GetComponent<EnemyMovement>();
        enemy = GetComponent<Enemy>();

        //if (healthBar == null) healthBar = GetComponentInChildren<Slider>();
        //healthBar.maxValue = healthData.MaxHealth;
        //healthBar.value = healthData.MaxHealth;

        //health = healthData.MaxHealth;

        //TakeDamageEvent += HandlePain;
    }

    public override void HandlePain()
    {
        // you can do some cool stuff based on the
        // amount of damage taken relative to max health
        // here we're simply setting the additive layer
        // weight based on damage vs max pain threshhold

        // spawn random hit particles
        int x = UnityEngine.Random.Range(0, ParticleManager.Instance.HitParticles.Count);
        Instantiate(ParticleManager.Instance.HitParticles[x], transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
    }

    public override void HandleDeath()
    {
        enemy.IsAlive = false;

        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        ParticleManager.Instance.SpawnEnemyDeathParticle(transform);

        Movement.StopMoving();
        enemy.Animator.SetInteger("Movement", -1);
        enemy.Animator.applyRootMotion = true;
        enemy.Animator.Play("Dying");

        // event comes from inherited class
        DeathEvent?.Invoke();
    }

    // called as animation event
    private void RemoveEnemyObject()
    {
        gameObject.SetActive(false);
    }

    //private void OnDisable()
    //{
    //    TakeDamageEvent -= HandlePain;
    //}
}
