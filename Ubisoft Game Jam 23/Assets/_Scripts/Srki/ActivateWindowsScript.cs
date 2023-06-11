using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class ActivateWindowsScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;

    private void Start()
    {
        group.alpha = 0f;
    }

    private void OnEnable()
    {
        EventManager.ActivateWindowsEvent += ActivatePanel;
        EventManager.GameCompleteEvent += DeactivatePanel;
    }

    private void OnDisable()
    {
        EventManager.ActivateWindowsEvent -= ActivatePanel;
        EventManager.GameCompleteEvent -= DeactivatePanel;
    }

    private void ActivatePanel()
    {
        StartCoroutine(PanelFade());
    }

    private IEnumerator PanelFade()
    {
        while(group.alpha < 1f)
        {
            group.alpha += Time.deltaTime;
            yield return null;
        }
    }

    private void DeactivatePanel()
    {
        group.alpha = 0f;
    }
}
