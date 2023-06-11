using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEngine;

public class BSODSequence : MonoBehaviour
{
    [SerializeField] private GameObject BSODcanvas;
    [SerializeField] private GameObject blackCanvas;

    [SerializeField] private CanvasGroup automaticRepairCanvas;

    [SerializeField] private float secondsToWait = 7f;

    private void OnEnable()
    {
        EventManager.GameCompleteEvent += StartBSOD;
    }

    private void OnDisable()
    {
        EventManager.GameCompleteEvent -= StartBSOD;
    }

    private void StartBSOD()
    {
        BSODcanvas.SetActive(true);

        Player.Sound.PlayGameWin();

        StartCoroutine(EndBSOD());
    }

    private IEnumerator EndBSOD()
    {
        yield return new WaitForSeconds(secondsToWait);

        BSODcanvas.SetActive(false);
        blackCanvas.SetActive(true);

        while (automaticRepairCanvas.alpha < 1f)
        {
            automaticRepairCanvas.alpha += Time.deltaTime * 2;
            yield return null;
        }

        blackCanvas.SetActive(false);
    }

    public void RestartGameButton()
    {
        Reset();
        SoundManager.Instance.PlayMainMusic();
        GameFlowManager.ResetGame();
    }

    private void Reset()
    {
        BSODcanvas.gameObject.SetActive(false);
        blackCanvas.gameObject.SetActive(false);
        automaticRepairCanvas.alpha = 0;
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
