using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class PlayerGunParticles : MonoBehaviour
{
    private ParticleSystem particles;

    private void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }

    public void Play()
    {
        particles.Stop();
        particles.Play();
    }
}
