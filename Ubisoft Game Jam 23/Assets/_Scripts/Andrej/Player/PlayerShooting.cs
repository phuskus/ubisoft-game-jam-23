using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class PlayerShooting : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public float ReloadTime = 0.25f;

    private float reloadTimer;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        reloadTimer = ReloadTime;
    }

    private void Update()
    {
        reloadTimer -= Time.deltaTime;

        if (reloadTimer > 0f) return;

        if(Input.GetMouseButton(0))
        {
            animator.Play("Player Shoot", 0, 0);
            Instantiate(ProjectilePrefab, Player.Gun.transform.position, transform.rotation);
            Player.Gun.Particles.Play();
            reloadTimer = ReloadTime;
        }
    }
}
