using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBlock : MonoBehaviour
{
	public List<Transform> PassageBlockers;

	private void Start()
	{
		foreach (Transform p in PassageBlockers)
		{
			p.gameObject.SetActive(true);
		}
	}
}
