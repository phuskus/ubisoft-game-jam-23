using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    //[Header("Level end explosion particles:")]
    //[SerializeField] private List<ParticleSystem> levelEndExplosions = new List<ParticleSystem>();
    //[SerializeField] private GameObject 
    private List<ParticleSystem> shuffledExplosions;    // a shuffled list of explosions
    [SerializeField] private float explosionWaitTime = 0.3f;

    [Space(15)]
    [Header("When enemy shot particles")]
    [SerializeField] private List<ParticleSystem> hitParticles;
    public List<ParticleSystem> HitParticles { get => hitParticles; }

    [Space(10)]
    [SerializeField] private GameObject enemyDeathParticles;
    [SerializeField] private GameObject playerDeathParticles;
    [SerializeField] private GameObject playerDeathSmokeParticles;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public IEnumerator PlayLevelEndExplosions(List<ParticleSystem> explosions)
    {
        ShuffleExplosionsOrder(explosions);

        yield return new WaitForSeconds(explosionWaitTime * 2);

        for (int i = 0; i < shuffledExplosions.Count; i++)
        {
            yield return new WaitForSeconds(explosionWaitTime);            
            shuffledExplosions[i].Play();
        }

        yield break;
    }

    private void ShuffleExplosionsOrder(List<ParticleSystem> explosions)
    {
        System.Random rand = new System.Random();
        shuffledExplosions = explosions.OrderBy(_ => rand.Next()).ToList();
    }

    public void SpawnEnemyDeathParticle(Transform trans)
    {
        Instantiate(enemyDeathParticles, trans.position, Quaternion.identity);
    }
    
    public void SpawnPlayerDeathParticles(Transform trans)
    {
        Instantiate(playerDeathParticles, trans.position, Quaternion.identity);
        Instantiate(playerDeathSmokeParticles, trans.position, trans.rotation);
    }
}
