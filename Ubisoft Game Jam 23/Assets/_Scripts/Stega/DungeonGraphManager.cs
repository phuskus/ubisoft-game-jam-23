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
    public static int CurrentDungeonDifficulty = -1;
    
    [SerializeField] private GraphNode graphNodePrefab;
    [SerializeField] private Transform startNodeAnchor;
    [SerializeField] private Canvas canvas;
    
    public static int CurrentLevel;
    private static int currentDungeonDifficulty;
    private static bool[][] grid = null;

    private void Start()
    {
        InstantiateGraphView();
    }

    private void InstantiateGraphView()
    {
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
                if (!grid[row][col])
                {
                    gridAsNodes[row].Add(null);
                    continue;
                }

                Vector3 position = startNodeAnchor.position + Vector3.up * nodeHeight * 1.5f * (row + 1);
                position.x = leftXStart + col * nodeWidth * 5f;
                GraphNode node = MakeGraphNode(position, row);
                node.gameObject.name = $"Node_{row}_{col}";
                gridAsNodes[row].Add(node);
            }
        }

        GraphNode startNode = MakeGraphNode(startNodeAnchor.position, -1);
        startNode.SetNumberOfHops(-1);
        startNode.gameObject.name = "StartNode";
        GraphNode endNode = MakeGraphNode(startNodeAnchor.position + Vector3.up * nodeHeight * 1.5f * (graphHeight + 1), graphHeight);
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
            
            previous.SetNumberOfHops(previousNodeRow+1);
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
        currentDungeonDifficulty = -1;
        GenerateGraph();
    }

    private static void GenerateGraph()
    {
        grid = new bool[graphHeight][];
        for (int row = 0; row < graphHeight; row++)
        {
            grid[row] = new bool[graphWidth];
            bool atLeastOne = false;
            for (int col = 0; col < graphWidth; col++)
            {
                float rand = Random.value;
                if (rand < 0.75f)
                {
                    grid[row][col] = false;
                }
                else
                {
                    grid[row][col] = true;
                    atLeastOne = true;
                }
            }

            if (!atLeastOne)
            {
                grid[row][Random.Range(0, graphWidth)] = true;
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
