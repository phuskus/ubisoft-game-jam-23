using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomTextProjectile : MonoBehaviour
{
    private TextMeshProUGUI randText;

    [SerializeField] private List<string> textList = new();

    void Start()
    {
        randText = GetComponent<TextMeshProUGUI>();
        randText.text = textList[Random.Range(0, textList.Count)];
    }
}
