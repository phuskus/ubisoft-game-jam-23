using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class PlayerMesh : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayDeathAnimation()
    {
        animator.Play("PlayerDeath", 0, 0f);
    }
}
