using System.Collections.Generic;
using UnityEngine;

public class GridBlock : MonoBehaviour
{
    private int _playerLayer;
    public bool Triggered { get; set; }

    [SerializeField] private List<Transform> obstaclePostions = new();
    [Space(10)]
    [SerializeField] private List<GameObject> obstaclePrefabs;

    [Space(10)]
    [Header("Enemy Settings")]
    [SerializeField] private List<Transform> enemySpawnPositions = new();
    [SerializeField] private Enemy enemyPrefab;

    [Space(10)]
    [SerializeField] private List<Enemy> enemies = new();
    private int enemyDeathCounter;

    public bool BlockCleared { get; private set; }

    [Space(10)]
    [Header("Passage Blockers")]
    [SerializeField] private List<BlockPassage> passageBlockers;

    // Call for enabling of this script before the player enters the segment
    private void Start()
    {
        if (obstaclePrefabs != null)
        {
            //SpawnObstacles();
        }

        SpawnEnemies();

        _playerLayer = LayerMask.NameToLayer("Player");

        // Disable all the enemies and wait for player to enter the room
        ChangeEnemyActiveStateTo(false);
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

            Enemy enemy = Instantiate(enemyPrefab, enemySpawnPositions[randomPosition]);

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
            BlockCleared = true;

            // When all enemies are cleared -> activate ramp
            foreach (BlockPassage passage in passageBlockers)
            {
                // Open -> Move animation
                passage.OpenPassage();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _playerLayer
            )
        {
            print("da");
            // Activate all the remaining enemies
            foreach (Enemy enemy in enemies)
            {
                if (enemy.IsAlive)
                {
                    enemy.gameObject.SetActive(true);
                }
            }

            Triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _playerLayer
            && Triggered
                )
        {
            // When leaving the room, check if there are any enemies alive
            // and disable them
            ChangeEnemyActiveStateTo(false);
        }
    }

    private void ChangeEnemyActiveStateTo(bool isActive)
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy.IsAlive != isActive)
            {
                enemy.gameObject.SetActive(isActive);
            }
        }
    }

    //private void OnDisable()
    //{
    //    for (int i = 0; i < enemies.Count; i++)
    //    {
    //        // listen to the enemy's death event
    //        enemies[i].GetComponent<EnemyHealth>().DeathEvent -= CountEnemyDeaths;
    //    }
    //}
}
