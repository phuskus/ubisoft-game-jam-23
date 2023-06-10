using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphNode : MonoBehaviour
{
	public int MyLevel;
	
	[SerializeField] private int numberOfHops;
	[SerializeField] private TextMeshProUGUI textNumberOfHops;
	[SerializeField] private GraphLine linePrefab;
	
	public List<GraphNode> ConnectedNodes;
	private RectTransform rectTransform;
	private Button button;
	
	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		button = GetComponent<Button>();

		button.onClick.AddListener(OnClick);
		
		SetNumberOfHops(numberOfHops);

		foreach (GraphNode node in ConnectedNodes)
		{
			GraphLine line = Instantiate(linePrefab, transform.parent).GetComponent<GraphLine>();
			line.transform.SetAsFirstSibling();
			line.SetPoints(rectTransform.position, node.transform.position);
		}
	}

	private void OnClick()
	{
		DungeonGraphManager.HopLevels(numberOfHops);
	}

	private void Update()
	{
		button.interactable = DungeonGraphManager.CurrentLevel == MyLevel;
	}

	public void SetNumberOfHops(int newNumber)
	{
		numberOfHops = newNumber;
		if (numberOfHops == -1)
		{
			// start node
			button.enabled = false;
			textNumberOfHops.text = "S";
		}
		else if (numberOfHops == 0)
		{
			// finish, root node
			button.enabled = true;
			textNumberOfHops.text = "R";
		}
		else
		{
			button.enabled = true;
			textNumberOfHops.text = $"{newNumber}";
		}
	}
}