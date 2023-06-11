using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class RestartingAnimation : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI text;
	[SerializeField] private float durationSeconds = 3f;
	
	private void OnEnable()
	{
		StartCoroutine(_Animation());
	}

	private string RepeatDots(int count)
	{
		if (count == 0)
			return "";
		
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < count; i++)
		{
			sb.Append(".");
		}
		return sb.ToString();
	}
	
	private IEnumerator _Animation()
	{
		float startTime = Time.time;
		int i = 0;
		while (true)
		{
			text.text = "Restarting" + RepeatDots(i);
			i++;
			if (i == 4)
			{
				i = 0;
			}

			if (Time.time - startTime > durationSeconds)
			{
				GameFlowManager.ResetGame();
				SoundManager.Instance.PlayMainMusic();
				break;
			}
			
			yield return new WaitForSeconds(0.5f);
		}
	}
}
