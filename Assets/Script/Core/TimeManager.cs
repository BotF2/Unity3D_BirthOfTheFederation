using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using Assets.Core;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    
    public event Action<TrekRandomEventSO> OnRandomSpecialEvent; // 
    public Action<TrekRandomEventSO> onRandomSpecialEvent; // instance of the delegate Action 
    public event Action<TrekStardateEventSO> OnStardateSpecialEvent; // 
    public Action<TrekStardateEventSO> onStardateSpecialEvent;
    public event Action OnStardateChanged; //StardateUIController subscribes the UpdateDateText() function
    private float timer;
    public int currentStardate { get; private set; }
    private Coroutine timeCoroutine;
    private float timeSpeedReducer = 10f;
    public List<TrekRandomEventSO> randomEvents;
    public List<TrekStardateEventSO> stardateEvents;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        GameManager.Instance.timeManager = this;
        timer = timeSpeedReducer;
        timeCoroutine = StartCoroutine(TimeProgression());
        currentStardate = 1010;
    }

    void Update()
    {

    }
    private System.Collections.IEnumerator TimeProgression()
    {

        while (MainMenuUIController.instance.PastMainMenu) 
        {
            yield return new WaitForSeconds(10f / timeSpeedReducer); // 10 seconds in game = 1 oneInXChance
            // Increment current day
            //currentDay++;
            currentStardate++;
            OnStardateChanged?.Invoke();

            // Check for special events
            CheckSpecialEvents();
        }
        
    }

    // Check for special events and trigger corresponding actions
    private void CheckSpecialEvents()
    {
        foreach (var specialEvent in randomEvents)
        {
            if (specialEvent != null)
            {
                if (1 == UnityEngine.Random.Range(1, specialEvent.oneInXChance))
                {
                    // Trigger special event
                    onRandomSpecialEvent?.Invoke(specialEvent);
                }
            }
        }
        foreach (var specialEvent in stardateEvents)
        {
            if (specialEvent != null && currentStardate == specialEvent.stardate)
            {
                // Trigger special event
                onStardateSpecialEvent?.Invoke(specialEvent);
            }
        }
    }

    // Method to set time speed multiplier
    public void SetTimeSpeedMultiplier(float multiplier)
    {
        if (multiplier > 0)
            timeSpeedReducer = multiplier;

        // Restart time progression coroutine with new speed multiplier
        if (timeCoroutine != null)
        {
            StopCoroutine(timeCoroutine);
            timeCoroutine = StartCoroutine(TimeProgression());
        }
    }

    // Method to pause time progression
    public void PauseTime()
    {
        if (timeCoroutine != null)
            StopCoroutine(timeCoroutine);
    }

    // Method to resume time progression
    public void ResumeTime()
    {
        timeCoroutine = StartCoroutine(TimeProgression());
    }

    // Method to get current oneInXChance
    public int GetCurrentDay()
    {
        return currentStardate;
    }
}

