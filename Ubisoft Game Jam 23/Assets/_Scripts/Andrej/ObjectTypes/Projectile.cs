using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class Projectile : MonoBehaviour
{
    public int WallLayer;
    private float currentSpeed;

    [SerializeField] bool isPlayerProjectile;
    private int _enemyLayer;

    [SerializeField] private int damageValue = 1;

    private void Start()
    {
        currentSpeed = Player.Settings.ProjectileSpeedStart;
        if (isPlayerProjectile)
        {
            _enemyLayer = LayerMask.NameToLayer("Enemy");
        }
        else _enemyLayer = LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        currentSpeed = Mathf.MoveTowards(currentSpeed, Player.Settings.ProjectileSpeedEnd, Player.Settings.ProjectileWeight * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _enemyLayer)
        {
            //Damageable damageable = other.gameObject.GetComponent<Damageable>();

            //damageable.TakeDamage(damageValue);
            if (other.gameObject.TryGetComponent(out Damageable damageable))
            {
                damageable.TakeDamage(damageValue);
            }

            // Spawn particles

            gameObject.SetActive(false);
        }

        if (other.gameObject.layer == WallLayer)
        {
            // Spawn particles

            gameObject.SetActive(false);
        }
    }
}
