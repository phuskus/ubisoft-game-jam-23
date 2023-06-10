using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Child class of Damageable
/// </summary>

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class EnemyHealth : Damageable
{
    public int Health { get => health; set => health = value; } // property of inherited field

    [SerializeField] private EnemyMovement Movement;
    [SerializeField] private Animator Animator;

    private void Start()
    {
        Movement = GetComponent<EnemyMovement>();
        Animator = GetComponent<Animator>();

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

        //Animator.ResetTrigger("Hit");
        //Animator.SetLayerWeight(1, (float)Damage / MaxDamagePainThreshold);
        //Animator.SetTrigger("Hit");
    }

    public override void HandleDeath()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        ParticleManager.Instance.SpawnEnemyDeathParticle(transform);

        Movement.StopMoving();
        Animator.SetInteger("Movement", -1);
        Animator.applyRootMotion = true;
        Animator.Play("Dying");

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
