using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridBlock : MonoBehaviour
{
    public static readonly int2[] DIRECTIONS =
    {
        new(0, 1),
        new(0, -1),
        new(1, 0),
        new(-1, 0),
    };
    
    private int _playerLayer;

    public int2 Coords;

    [SerializeField] private List<Transform> obstaclePostions = new();
    [Space(10)]
    [SerializeField] private List<GameObject> obstaclePrefabs;

    [Space(10)]
    [Header("Enemy Settings")]
    //[SerializeField] private Enemy enemyPrefab;
    public Enemy regularEnemyPrefab;
    public Enemy eliteEnemyPrefab;

    [Space(10)]
    [SerializeField] private List<Enemy> enemies = new();

    public bool BlockCleared;

    [Space(10)]
    [Header("Passage Blockers")]
    [SerializeField] private List<BlockPassage> passageBlockers;

    private bool blockIsActive;

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
        int enemiesNumber = Random.Range(3, 3 + DungeonGraphManager.CurrentDungeonDifficulty * 4);

        for (int i = 0; i < enemiesNumber; i++)
        {
            Vector3 randomPosition = transform.position + new Vector3(Random.Range(-8, 9), 0, Random.Range(-8, 9));
            Enemy enemy;
            
            if (Random.value < 0.2f)
            {
                // Elite
                enemy = Instantiate(eliteEnemyPrefab, randomPosition, Quaternion.identity, transform);
            }
            else
            {
                // Regular
                enemy = Instantiate(regularEnemyPrefab, randomPosition, Quaternion.identity, transform);
            }

            // add enemy to list
            enemies.Add(enemy);
        }
    }

    private void CheckEnemyDeaths()
    {
        bool haveAliveEnemies = false; 

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].IsAlive) haveAliveEnemies = true;
        }

        if(!haveAliveEnemies)
        {
            EventManager.EnemyDeathEvent -= CheckEnemyDeaths;
            BlockCleared = true;
            DungeonLevelManager.RoomsLeftToClear--;
            OpenAllValidDoors();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (blockIsActive || BlockCleared)
            return;
        
        if (other.gameObject.layer == _playerLayer)
        {
            blockIsActive = true;
            ChangeEnemyActiveStateTo(true);
            CloseAllDoors();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (blockIsActive && !BlockCleared)
            return;
        
        blockIsActive = false;
        
        if (other.gameObject.layer == _playerLayer)
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
            if (enemy.IsAlive)
            {
                enemy.gameObject.SetActive(isActive);
                if (isActive)
                    enemy.PlayAppearAnimation();
            }
        }
    }

    private void OnEnable()
    {
        EventManager.EnemyDeathEvent += CheckEnemyDeaths;
    }

    public void SetPassageOpenState(int2 side, bool state)
    {
        // north, east, south, west
        if (side.x == 0 && side.y == 1)
        {
            passageBlockers[0].SetOpenState(state);
        }
        
        if (side.x == 1 && side.y == 0)
        {
            passageBlockers[1].SetOpenState(state);
        }
        
        if (side.x == 0 && side.y == -1)
        {
            passageBlockers[2].SetOpenState(state);
        }
        
        if (side.x == -1 && side.y == 0)
        {
            passageBlockers[3].SetOpenState(state);
        }
    }

    public void CloseAllDoors()
    {
        SoundManager.Instance.PlayDoorClose();
        foreach (BlockPassage p in passageBlockers)
        {
            p.SetOpenState(false);
        }
    }

    public void OpenAllValidDoors()
    {
        SoundManager.Instance.PlayDoorOpen();
        foreach (int2 dir in DIRECTIONS)
        {
            int2 pos = Coords + dir;
            if (DungeonLevelManager.BlockCoords.Contains(pos))
            {
                SetPassageOpenState(dir, true);
            }
        }
    }
}
