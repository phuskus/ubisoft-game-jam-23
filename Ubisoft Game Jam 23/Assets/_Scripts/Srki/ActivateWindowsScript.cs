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
    }

    private void OnDisable()
    {
        EventManager.ActivateWindowsEvent -= ActivatePanel;
    }

    private void ActivatePanel()
    {
        StartCoroutine(PanelFade());
    }

    private IEnumerator PanelFade()
    {
        while(group.alpha < 100f)
        {
            group.alpha += Time.deltaTime;
            yield return null;
        }
    }
}
