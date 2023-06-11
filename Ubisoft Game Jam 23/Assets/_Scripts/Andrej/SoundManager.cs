using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class SoundManager : SingletonMono<SoundManager>
{
    private AudioSource gameAudio;

    public SoundEffect DoorOpen;
    public SoundEffect DoorClose;
    public List<SoundEffect> EnemyAttacks;
    public SoundEffect LevelSelect;
    public SoundEffect LevelWin;
    public SoundEffect PlayerFire;

    private void Start()
    {
        gameAudio = Camera.main.GetComponent<AudioSource>();
    }


}
