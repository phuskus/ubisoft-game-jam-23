using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class Projectile : MonoBehaviour
{
    public int WallLayer;
    private float currentSpeed;

    private void Start()
    {
        currentSpeed = Player.Settings.ProjectileSpeedStart;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        currentSpeed = Mathf.MoveTowards(currentSpeed, Player.Settings.ProjectileSpeedEnd, Player.Settings.ProjectileWeight * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == WallLayer)
        {
            Destroy(gameObject);
        }
    }
}
