using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

[System.Serializable]
public class SoundEffect
{
    public AudioClip Clip;
    public float Volume;

    public void Play(AudioSource source)
    {
        source.PlayOneShot(Clip, Volume);
    }
}
