using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class DungeonGraphManager : MonoBehaviour
{
    private static DungeonGraphManager I;
    
    private const int graphWidth = 3;
    private const int graphHeight = 5;
    
    public static int CurrentDungeonDifficulty = -1;
    
    [SerializeField] private GraphNode graphNodePrefab;
    [SerializeField] private Transform startNodeAnchor;
    [SerializeField] private Canvas canvas;
    
    public static int CurrentLevel;
    private static bool[][] grid = null;

    private void Start()
    {
        // Reset();
        InstantiateGraphView();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Reset();
            InstantiateGraphView();
        }
    }

    private void InstantiateGraphView()
    {
        foreach (Transform child in canvas.transform)
        {
            if (child.GetComponent<GraphNode>() != null || child.GetComponent<GraphLine>() != null)
            {
                Destroy(child.gameObject);
            }
        }
        
        Rect nodeRect = graphNodePrefab.GetComponent<RectTransform>().rect;
        float nodeHeight = nodeRect.height;
        float nodeWidth = nodeRect.width;
        float leftXStart = startNodeAnchor.position.x - (graphWidth / 2) * nodeWidth * 5f;

        List<List<GraphNode>> gridAsNodes = new();
        for (int i = 0; i < graphHeight; i++)
        {
            gridAsNodes.Add(new());
        }
        
        for (int row = 0; row < graphHeight; row++)
        {
            for (int col = 0; col < graphWidth; col++)
            {
                if (!grid[col][row])
                {
                    gridAsNodes[row].Add(null);
                    continue;
                }

                Vector3 position = startNodeAnchor.position + Vector3.up * nodeHeight * 2f * (row + 1);
                position.x = leftXStart + col * nodeWidth * 5f;
                GraphNode node = MakeGraphNode(position, row);
                node.gameObject.name = $"Node_{row}_{col}";
                gridAsNodes[row].Add(node);
            }
        }

        GraphNode startNode = MakeGraphNode(startNodeAnchor.position, -1);
        startNode.SetNumberOfHops(-1);
        startNode.gameObject.name = "StartNode";
        GraphNode endNode = MakeGraphNode(startNodeAnchor.position + Vector3.up * nodeHeight * 2f * (graphHeight + 1), graphHeight);
        endNode.SetNumberOfHops(0);
        endNode.gameObject.name = "EndNode";
        
        for (int col = 0; col < graphWidth; col++)
        {
            int emptyCount = 0;
            GraphNode previous = endNode;
            int previousNodeRow = graphHeight;
            for (int row = graphHeight-1; row >= 0; row--)
            {
                GraphNode node = gridAsNodes[row][col];
                if (node == null)
                {
                    emptyCount++;
                    continue;
                }
                
                node.ConnectedNodes.Add(previous);
                node.SetNumberOfHops(emptyCount+1);
                emptyCount = 0;
                previous = node;
                previousNodeRow = row;
            }
            
            // previous.SetNumberOfHops(previousNodeRow+1);
            startNode.ConnectedNodes.Add(previous);
        }
    }

    private GraphNode MakeGraphNode(Vector3 position, int level)
    {
        GraphNode node = Instantiate(graphNodePrefab, position, Quaternion.identity, canvas.transform);
        node.MyLevel = level;
        return node;
    }

    public static void Reset()
    {
        CurrentLevel = 0;
        CurrentDungeonDifficulty = -1;
        GenerateGraph();
    }

    private static void GenerateGraph()
    {
        grid = new bool[graphWidth][];
        for (int col = 0; col < graphWidth; col++)
        {
            grid[col] = new bool[graphHeight];
            bool atLeastOne = false;
            for (int row = 0; row < graphHeight; row++)
            {
                float rand = Random.value;
                if (rand < 0.5f)
                {
                    grid[col][row] = false;
                }
                else
                {
                    grid[col][row] = true;
                    atLeastOne = true;
                }
            }

            if (!atLeastOne)
            {
                grid[col][Random.Range(0, graphWidth)] = true;
            }
        }
    }

    public static void LoadDungeon(int difficulty)
    {
        CurrentDungeonDifficulty = difficulty;
        SceneManager.LoadScene("DungeonLevel");
    }

    public static void OnDungeonCompleted()
    {
        if (CurrentDungeonDifficulty == 0)
        {
            // end game
            Debug.Log("Victory!");
            GameFlowManager.OnVictory();
        }
        CurrentLevel += CurrentDungeonDifficulty;
        SceneManager.LoadScene("DungeonGraph");
    }
}
