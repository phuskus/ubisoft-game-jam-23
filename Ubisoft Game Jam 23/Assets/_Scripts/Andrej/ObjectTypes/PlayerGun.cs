using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class PlayerGun : SingletonMono<PlayerGun>
{
    public PlayerGunParticles Particles;

    private void Start()
    {
        Particles = GetComponentInChildren<PlayerGunParticles>();
    }
}
