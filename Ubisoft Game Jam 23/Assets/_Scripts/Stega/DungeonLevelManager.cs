using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonLevelManager : MonoBehaviour
{
    public static DungeonLevelManager I { get; private set; }
    public static int RoomCount = 10;
    
    [SerializeField] private GridBlock gridBlockPrefab;
    [SerializeField] private GridBlock gridBlockEndPrefab;
    
    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
    }

    private void Start()
    {
        GenerateDungeon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateDungeon();
        }
    }

    private void GenerateDungeon()
    {
        foreach (GridBlock o in FindObjectsOfType<GridBlock>())
        {
            Destroy(o.gameObject);
        }
        
        int2[] directions =
        {
            new(0, 1),
            new(0, -1),
            new(1, 0),
            new(-1, 0),
        };

        GridBlock firstBlock = Instantiate(gridBlockPrefab, Vector3.zero, Quaternion.identity).GetComponent<GridBlock>();
        Vector3 gridBlockSize = new Vector3(10, 0, 10);
        
        int2 currentPoint = int2.zero;
        List<int2> previousPoints = new() { currentPoint };
        for (int i = 0; i < RoomCount - 1; i++)
        {
            int2 dir;
            int2 newPoint;
            int idx = Random.Range(0, directions.Length);
            bool found = false;
            for (int j = 0; j < directions.Length; j++)
            {
                dir = directions[idx];
                newPoint = currentPoint + dir;
                if (!previousPoints.Exists(p => p.x == newPoint.x && p.y == newPoint.y))
                {
                    currentPoint = newPoint;
                    found = true;
                    break;
                }

                idx++;
                if (idx >= directions.Length)
                {
                    idx = 0;
                }
            }

            if (!found)
            {
                break;
            }

            previousPoints.Add(currentPoint);

            Vector3 position = new Vector3(gridBlockSize.x * currentPoint.x, 0, gridBlockSize.z * currentPoint.y);
            GridBlock prefab = i == RoomCount - 2 ? gridBlockEndPrefab : gridBlockPrefab;
            GridBlock block = Instantiate(prefab, position, Quaternion.identity).GetComponent<GridBlock>();
        }
    }
}
