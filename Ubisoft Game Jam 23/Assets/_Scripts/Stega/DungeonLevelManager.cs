using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonLevelManager : MonoBehaviour
{
    public static DungeonLevelManager I { get; private set; }
    public static int RoomCount = 5;
    public static int RoomsLeftToClear;
    public static List<int2> BlockCoords;
    
    [SerializeField] private GridBlock gridBlockPrefab;
    [SerializeField] private GridBlock gridBlockEndPrefab;
    [SerializeField] private GridBlock gridBlockStartPrefab;
    [SerializeField] private TextMeshProUGUI textRoomsCleared;
    
    
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
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     GenerateDungeon();
        // }

        textRoomsCleared.text = $"Rooms left: {RoomsLeftToClear}";
    }

    private void GenerateDungeon()
    {
        foreach (GridBlock o in FindObjectsOfType<GridBlock>())
        {
            Destroy(o.gameObject);
        }

        RoomsLeftToClear = RoomCount - 1;
        BlockCoords = new List<int2>();

        List<GridBlock> blocks = new List<GridBlock>();
        
        GridBlock firstBlock = Instantiate(gridBlockStartPrefab, Vector3.zero, Quaternion.identity).GetComponent<GridBlock>();
        firstBlock.gameObject.name = "Grid_Block_First";
        firstBlock.BlockCleared = true;
        firstBlock.Coords = int2.zero;
        
        BlockCoords.Add(int2.zero);
        blocks.Add(firstBlock);
        
        Vector3 gridBlockSize = new Vector3(20, 0, 20);
        
        int2 currentPoint = int2.zero;
        for (int i = 0; i < RoomCount - 1; i++)
        {
            int2 dir = default;
            int2 newPoint;
            int idx = Random.Range(0, GridBlock.DIRECTIONS.Length);
            bool found = false;
            for (int j = 0; j < GridBlock.DIRECTIONS.Length; j++)
            {
                dir = GridBlock.DIRECTIONS[idx];
                newPoint = currentPoint + dir;
                if (!BlockCoords.Exists(p => p.x == newPoint.x && p.y == newPoint.y))
                {
                    currentPoint = newPoint;
                    found = true;
                    break;
                }

                idx++;
                if (idx >= GridBlock.DIRECTIONS.Length)
                {
                    idx = 0;
                }
            }

            if (!found)
            {
                break;
            }

            BlockCoords.Add(currentPoint);

            Vector3 position = new Vector3(gridBlockSize.x * currentPoint.x, 0, gridBlockSize.z * currentPoint.y);
            GridBlock prefab = i == RoomCount - 2 ? gridBlockEndPrefab : gridBlockPrefab;
            GridBlock block = Instantiate(prefab, position, Quaternion.identity).GetComponent<GridBlock>();
            block.Coords = currentPoint;
            blocks.Add(block);
            block.gameObject.name = $"Grid_Block_{position}";
            
            if (i == 0)
            {
                block.SetPassageOpenState(-dir, true);
                firstBlock.SetPassageOpenState(dir, true);
            }
        }

        foreach (GridBlock b in blocks)
        {
            b.OpenAllValidDoors();
        }
    }
}
