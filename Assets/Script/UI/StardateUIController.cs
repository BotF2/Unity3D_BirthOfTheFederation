using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StardateUIController : MonoBehaviour
{
    public TextMeshProUGUI stardateText;

    private void OnEnable()
    {
        // Subscribe to the event
        TimeManager.instance.OnStardateChanged += UpdateDateText;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks or errors when the object is destroyed
        TimeManager.instance.OnStardateChanged -= UpdateDateText;
    }
    void UpdateDateText()
    {
        stardateText.text = TimeManager.instance.currentStardate.ToString();
    }
}
