using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFlowManager : MonoBehaviour
{
	public static GameFlowManager I { get; private set; }
	
	[SerializeField] private float gameDurationSeconds = 75;
	[SerializeField] private TextMeshProUGUI textTimer;
	[SerializeField] private Image defeatPanel;
	

	private float secondsLeft;

	private bool timerEnabled;
	private Tween tween;

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
		I.textTimer.gameObject.SetActive(true);
		I.secondsLeft = I.gameDurationSeconds;
		I.timerEnabled = true;
		I.defeatPanel.gameObject.SetActive(false);
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
			OnDefeat();
			return;
		}
		
		int secondsLeftAsInt = Mathf.CeilToInt(secondsLeft);
		int minutesLeft = secondsLeftAsInt / 60;
		secondsLeftAsInt %= 60;

		textTimer.text = $"{minutesLeft:D2}:{secondsLeftAsInt:D2}";
	}

	public void ReduceTime()
	{
		secondsLeft -= 10;
		if (tween == null
			|| !tween.active
            )
		{
            tween = textTimer.rectTransform.DOPunchScale(Vector3.one, 0.2f);
        }
    }

	public static void OnVictory()
	{
		I.textTimer.gameObject.SetActive(false);
		DungeonLevelManager.I.DestroyLevel();
		SoundManager.Instance.StopAllSounds();
		EventManager.GameCompleteEvent?.Invoke();
	}

	public static void OnDefeat()
	{
		I.textTimer.gameObject.SetActive(false);
		DungeonLevelManager.I?.DestroyLevel();
		SoundManager.Instance.StopAllSounds();
		I.defeatPanel.gameObject.SetActive(true);
	}
}
