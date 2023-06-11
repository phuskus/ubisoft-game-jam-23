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
    private static List<List<GraphNode>> gridAsNodes;
    private static GraphNode startNode, endNode;
    public static int lastCompletedNodeRow;
    public static int lastCompletedNodeCol;

    private void Start()
    {
        Reset();
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
        float leftXStart = startNodeAnchor.position.x - (graphWidth / 2) * nodeWidth * 4f;

        gridAsNodes = new();
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
                position.x = leftXStart + col * nodeWidth * 4f;
                GraphNode node = MakeGraphNode(position, row, row, col);
                node.gameObject.name = $"Node_{row}_{col}";
                gridAsNodes[row].Add(node);
            }
        }

        startNode = MakeGraphNode(startNodeAnchor.position, -1, -1, -1);
        startNode.SetNumberOfHops(-1);
        startNode.gameObject.name = "StartNode";
        
        endNode = MakeGraphNode(startNodeAnchor.position + Vector3.up * nodeHeight * 2f * (graphHeight + 1), graphHeight, graphHeight, graphHeight);
        endNode.SetNumberOfHops(0);
        endNode.gameObject.name = "EndNode";
        
        for (int col = 0; col < graphWidth; col++)
        {
            int emptyCount = 0;
            GraphNode firstNode = null;
            GraphNode lastNode = null;
            int leftToDistribute = graphHeight;
            for (int row = 0; row < graphHeight; row++)
            {
                GraphNode node = gridAsNodes[row][col];
                if (node == null)
                {
                    emptyCount++;
                    continue;
                }

                if (firstNode == null)
                {
                    firstNode = node;
                }

                lastNode = node;
                
                node.SetNumberOfHops(emptyCount+1);
                leftToDistribute -= emptyCount + 1;
                emptyCount = 0;
            }
            
            lastNode!.SetNumberOfHops(lastNode.GetNumberOfHops() + leftToDistribute);
            startNode.ConnectedNodes.Add(firstNode);
            lastNode!.ConnectedNodes.Add(endNode);

            lastNode = endNode;
            for (int row = graphHeight - 1; row >= 0; row--)
            {
                GraphNode node = gridAsNodes[row][col];
                if (node == null)
                    continue;
                
                node.ConnectedNodes.Add(lastNode);
                lastNode = node;
            }
        }
    }

    private GraphNode MakeGraphNode(Vector3 position, int level, int row, int col)
    {
        GraphNode node = Instantiate(graphNodePrefab, position, Quaternion.identity, canvas.transform);
        node.MyLevel = level;
        node.Row = row;
        node.Col = col;
        return node;
    }

    public static void Reset()
    {
        CurrentLevel = 0;
        CurrentDungeonDifficulty = -1;
        lastCompletedNodeRow = -1;
        lastCompletedNodeCol = -1;
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

        lastCompletedNodeRow = -1;
        lastCompletedNodeCol = -1;
    }

    public static void LoadDungeon(GraphNode node)
    {
        CurrentDungeonDifficulty = node.GetNumberOfHops();
        if (node != endNode)
        {
            (lastCompletedNodeRow, lastCompletedNodeCol) = (node.Row, node.Col);
        }
        SceneManager.LoadScene("DungeonLevel");
    }

    public static void OnDungeonCompleted()
    {
        if (CurrentDungeonDifficulty == 0)
        {
            // end game
            Debug.Log("Victory!");
            GameFlowManager.OnVictory();
            return;
        }
        
        CurrentLevel += CurrentDungeonDifficulty;
        if(CurrentLevel > 1)
        {
            EventManager.ActivateWindowsEvent?.Invoke();
        }
        SceneManager.LoadScene("DungeonGraph");
    }

    public static bool IsNodeSelectable(GraphNode node)
    {
        if (lastCompletedNodeCol == -1)
        {
            return startNode.ConnectedNodes.Contains(node);
        }
        
        GraphNode lastCompletedNode = gridAsNodes[lastCompletedNodeRow][lastCompletedNodeCol];
        return lastCompletedNode.ConnectedNodes.Contains(node);
    }
}
