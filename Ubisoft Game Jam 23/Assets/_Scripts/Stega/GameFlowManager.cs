using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
	[SerializeField] private int gameDurationSeconds = 5;
	[SerializeField] private TextMeshProUGUI textTimer;

	private float startTime;
	private int lastSecondsElapsed;
	
	private void Start()
	{
		ResetGame();
	}

	public void ResetGame()
	{
		startTime = Time.time;
		lastSecondsElapsed = -1;
	}

	private void Update()
	{
		int secondsElapsed = Mathf.CeilToInt(Time.time - startTime);
		if (secondsElapsed != lastSecondsElapsed)
		{
			Debug.Log("Update");
			lastSecondsElapsed = secondsElapsed;
			
			int secondsLeft = gameDurationSeconds - Mathf.CeilToInt(Time.time - startTime);
			int minutesLeft = secondsLeft / 60;
			secondsLeft %= 60;

			textTimer.text = $"{minutesLeft:D2}:{secondsLeft:D2}";
		}
	}
}
