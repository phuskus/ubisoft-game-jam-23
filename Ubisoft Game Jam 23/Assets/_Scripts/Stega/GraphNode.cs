using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphNode : MonoBehaviour
{
	[SerializeField] private int numberOfHops;
	[SerializeField] private TextMeshProUGUI textNumberOfHops;
	[SerializeField] private GraphLine linePrefab;
	
	public List<GraphNode> ConnectedNodes;
	private RectTransform rectTransform;
	
	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		
		SetNumberOfHops(numberOfHops);

		foreach (GraphNode node in ConnectedNodes)
		{
			GraphLine line = Instantiate(linePrefab, transform.parent).GetComponent<GraphLine>();
			line.transform.SetAsFirstSibling();
			line.SetPoints(rectTransform.position, node.transform.position);
		}
	}

	public void SetNumberOfHops(int newNumber)
	{
		numberOfHops = newNumber;
		if (numberOfHops == -1)
		{
			// start node
			GetComponent<Button>().enabled = false;
			textNumberOfHops.text = "S";
		}
		else if (numberOfHops == 0)
		{
			// finish, root node
			GetComponent<Button>().enabled = true;
			textNumberOfHops.text = "R";
		}
		else
		{
			GetComponent<Button>().enabled = true;
			textNumberOfHops.text = $"{newNumber}";
		}
	}
}