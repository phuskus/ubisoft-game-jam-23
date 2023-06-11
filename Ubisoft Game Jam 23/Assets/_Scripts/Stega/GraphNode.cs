using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GraphNode : MonoBehaviour
{
	private static readonly string NAME_ROOT = "HKEY_CLASSES_ROOT";
	private static readonly string CHAR_POOL = "qwertyuiopasdfghjklzxcvbnm";
	
	public int MyLevel;
	
	[SerializeField] private int numberOfHops;
	[SerializeField] private TextMeshProUGUI textNumberOfHops;
	[SerializeField] private GraphLine linePrefab;
	
	public List<GraphNode> ConnectedNodes = new List<GraphNode>();
	public int Row, Col;
	private RectTransform rectTransform;
	private Button button;
	
	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		button = GetComponent<Button>();

		button.onClick.AddListener(OnClick);
		
		SetNumberOfHops(numberOfHops);
	}

	private void Start()
	{
		GenerateLines();
	}

	public void GenerateLines()
	{
		foreach (GraphNode node in ConnectedNodes)
		{
			GraphLine line = Instantiate(linePrefab, transform.parent).GetComponent<GraphLine>();
			line.transform.SetAsFirstSibling();
			line.SetPoints(transform.position, node.transform.position);
		}
	}

	private void OnClick()
	{
		SoundManager.Instance.PlayLevelSelect();
		DungeonGraphManager.LoadDungeon(this);
	}

	private void Update()
	{
		button.interactable = DungeonGraphManager.IsNodeSelectable(this);
	}

	public void SetNumberOfHops(int newNumber)
	{
		numberOfHops = newNumber;
		if (numberOfHops == -1)
		{
			// start node
			button.enabled = false;
			textNumberOfHops.text = ".";
		}
		else if (numberOfHops == 0)
		{
			// finish, root node
			button.enabled = true;
			textNumberOfHops.text = NAME_ROOT;
		}
		else
		{
			button.enabled = true;
			textNumberOfHops.text = GetRandomFolderName();
		}
	}

	private string GetRandomFolderName()
	{
		StringBuilder sb = new StringBuilder();

		for (int i = 0; i < numberOfHops; i++)
		{
			sb.Append("../");
		}
		sb.Remove(sb.Length - 1, 1);
		
		// if (Random.value < 0.5f)
		// {
		// 	sb.Append(".");
		// }
		//
		// int length = 4 + numberOfHops;
		// for (int i = 0; i < length; i++)
		// {
		// 	sb.Append(CHAR_POOL[Random.Range(0, CHAR_POOL.Length)]);
		// }
		
		return sb.ToString();
	}

	public int GetNumberOfHops() => numberOfHops;
}