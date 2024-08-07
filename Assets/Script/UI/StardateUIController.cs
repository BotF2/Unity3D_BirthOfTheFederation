using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class StardateUIController : MonoBehaviour
{
    public TextMeshProUGUI stardateText;

    private void OnEnable()
    {
        if (TimeManager.Instance != null)
        TimeManager.Instance.OnStardateChanged += UpdateDateText;
    }

    private void OnDisable()
    {
        if (TimeManager.Instance != null)
        TimeManager.Instance.OnStardateChanged -= UpdateDateText;
    }
    void UpdateDateText()
    {
        stardateText.text = TimeManager.Instance.currentStardate.ToString();
    }
}
