using System.Collections.Generic;
using UnityEngine;

public class GridBlock : MonoBehaviour
{
    [SerializeField] private List<Transform> obstaclePostions = new();
    [Space(10)]
    [SerializeField] private List<GameObject> obstaclePrefabs;

    [Space(10)]
    [Header("Enemy Settings")]
    [SerializeField] private List<Transform> enemySpawnPositions = new();
    [SerializeField] private GameObject enemyPrefab;

    [Space(10)]
    [SerializeField] private List<GameObject> enemies = new();
    private int enemyDeathCounter;

    [Space(10)]
    [Header("Passage Blockers")]
    [SerializeField] private List<GameObject> passageBlockers;

    // Call for enabling of this script before the player enters the segment
    private void Start()
    {
        if (obstaclePrefabs != null)
        {
            //SpawnObstacles();
        }

        SpawnEnemies();

        //if (ramp == null) ramp = GetComponentInChildren<Ramp>();
    }

    private void SpawnObstacles()
    {
        float randomChance = Random.Range(0f, 1f);

        if (randomChance >= 0.07f)
        {
            int randomPosition = Random.Range(0, obstaclePostions.Count);
            int randomPrefab = Random.Range(0, obstaclePrefabs.Count);

            Instantiate(obstaclePrefabs[randomPrefab], obstaclePostions[randomPosition]);
        }
    }

    private void SpawnEnemies()
    {
        int enemiesNumber = Random.Range(2, 3 + DungeonGraphManager.CurrentDungeonDifficulty * 2);

        for (int i = 0; i < enemiesNumber; i++)
        {
            int randomPosition = Random.Range(0, enemySpawnPositions.Count);

            GameObject enemy = Instantiate(enemyPrefab, enemySpawnPositions[randomPosition]);

            // add enemy to list
            enemies.Add(enemy);

            // listen to the enemy's death event
            enemy.GetComponent<EnemyHealth>().DeathEvent += CountEnemyDeaths;
        }
    }

    private void CountEnemyDeaths()
    {
        enemyDeathCounter++;

        if (enemyDeathCounter == enemies.Count)
        {
            // When all enemies are cleared -> activate ramp
            //StartCoroutine(ramp.ActivateRamp());

            foreach(GameObject passage in passageBlockers)
            {
                passage.SetActive(false);
            }
        }
    }
}
