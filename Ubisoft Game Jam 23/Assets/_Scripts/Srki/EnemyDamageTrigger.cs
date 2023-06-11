using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTrigger : MonoBehaviour
{
    private int playerLayer;
    private bool triggered = false;

    private float waitTime = 0.5f; // time until the trigger activates again

    [SerializeField] private Enemy enemy;
    public GameObject DamageParticles;

    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");

        if(enemy == null) Debug.Log("Component of type Enemy is not assigned");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == playerLayer
            && !triggered
            )
        {
            triggered = true;

            if (other.TryGetComponent(out Damageable damageableHP))
            {
                // deal damage to player from scriptable object from Enemy script

                Instantiate(DamageParticles, transform.position, Quaternion.identity);
                Player.Sound.PlayEnemyAttack();
                damageableHP.TakeDamage(enemy.AttackDamage);
            }
            //other.GetComponent<Damageable>().TakeDamage(enemy.AttackSO.Damage);

            StartCoroutine(WaitTimeForTrigger());
        }
    }

    private IEnumerator WaitTimeForTrigger()
    {
        yield return new WaitForSeconds(waitTime);

        // reset the trigger
        triggered = false;

        yield break;
    }

    private void OnEnable()
    {
        enemy.Health.DeathEvent += DeactivateObject;
    }

    private void OnDisable()
    {
        enemy.Health.DeathEvent -= DeactivateObject;
    }

    private void DeactivateObject()
    {
        this.gameObject.SetActive(false);
    }
}
