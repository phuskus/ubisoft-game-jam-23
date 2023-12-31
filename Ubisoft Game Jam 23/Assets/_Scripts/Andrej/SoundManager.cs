using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class SoundManager : SingletonMono<SoundManager>
{
    private AudioSource gameAudio;

    public AudioClip BossMusic;
    public AudioClip MainMusic;
    public float TransitionSpeed = 5f;

    public SoundEffect DoorOpen;
    public SoundEffect DoorClose;
    public List<SoundEffect> EnemyAttacks;
    public List<SoundEffect> PlayerAttacks;
    public SoundEffect LevelSelect;
    public SoundEffect LevelWin;
    public SoundEffect GameWin;

    private void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }

    private void PlaySound(SoundEffect sound)
    {
        sound.Play(gameAudio);
    }

    public void SwitchToBossMusic() => StartCoroutine(ChangeMusic());

    IEnumerator ChangeMusic()
    {
        while(gameAudio.volume > 0f)
        {
            gameAudio.volume = Mathf.MoveTowards(gameAudio.volume, 0f, TransitionSpeed * Time.deltaTime);
            yield return null;
        }

        gameAudio.clip = BossMusic;
        gameAudio.Play();

        while (gameAudio.volume <= 0.5f)
        {
            gameAudio.volume = Mathf.MoveTowards(gameAudio.volume, 0.5f, TransitionSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void PlayDoorOpen() => PlaySound(DoorOpen);
    public void PlayDoorClose() => PlaySound(DoorClose);
    public void PlayLevelSelect() => PlaySound(LevelSelect);
    public void PlayLevelWin() => PlaySound(LevelWin);
    public void PlayGameWin() => PlaySound(GameWin);
    public void PlayPlayerFire()
    {
        int randomIndex = Random.Range(0, PlayerAttacks.Count);
        PlaySound(PlayerAttacks[randomIndex]);
    }

    public void PlayEnemyAttack()
    {
        int randomIndex = Random.Range(0, EnemyAttacks.Count);
        PlaySound(EnemyAttacks[randomIndex]);
    }

    public void StopAllSounds()
    {
        gameAudio.Stop();
        StopAllCoroutines();
    }

    public void PlayMainMusic()
    {
        gameAudio.Stop();
        gameAudio.clip = MainMusic;
        gameAudio.volume = 1;
        gameAudio.Play();
    }

    public void PlayBossMusic()
    {
        gameAudio.Stop();
        gameAudio.clip = BossMusic;
        gameAudio.volume = 1;
        gameAudio.Play();
    }
}
