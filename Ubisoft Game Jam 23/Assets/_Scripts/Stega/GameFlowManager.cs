using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
	public static GameFlowManager I { get; private set; }
	
	[SerializeField] private float gameDurationSeconds = 75;
	[SerializeField] private TextMeshProUGUI textTimer;

	private float secondsLeft;
	private bool timerEnabled;

	private void Awake()
	{
		if (I != null)
		{
			Destroy(gameObject);
			return;
		}

		I = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		ResetGame();
	}

	public static void ResetGame()
	{
		I.secondsLeft = I.gameDurationSeconds;
		I.timerEnabled = true;
		DungeonGraphManager.Reset();
		SceneManager.LoadScene("DungeonGraph");
	}

	private void Update()
	{
		if (!timerEnabled)
			return;
		
		secondsLeft -= Time.deltaTime;
		if (secondsLeft <= 0)
		{
			ResetGame();
			return;
		}
		
		int secondsLeftAsInt = Mathf.CeilToInt(secondsLeft);
		int minutesLeft = secondsLeftAsInt / 60;
		secondsLeftAsInt %= 60;

		textTimer.text = $"{minutesLeft:D2}:{secondsLeftAsInt:D2}";
	}

	public static void OnVictory()
	{
		//ResetGame();
		EventManager.GameCompleteEvent?.Invoke();

    }
}
