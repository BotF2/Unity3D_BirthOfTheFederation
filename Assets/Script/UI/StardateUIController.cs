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
        if (TimeManager.instance != null)
        TimeManager.instance.OnStardateChanged += UpdateDateText;
    }

    private void OnDisable()
    {
        if (TimeManager.instance != null)
        TimeManager.instance.OnStardateChanged -= UpdateDateText;
    }
    void UpdateDateText()
    {
        stardateText.text = TimeManager.instance.currentStardate.ToString();
    }
}
