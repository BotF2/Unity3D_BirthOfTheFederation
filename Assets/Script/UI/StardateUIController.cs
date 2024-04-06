using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StardateUIController : MonoBehaviour
{
    public TextMeshProUGUI stardateText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
        stardateText.text = TimeManager.instance.currentDay.ToString();
    }
}
