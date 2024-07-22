using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using Assets.Core;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public event Action<TrekEventSO> OnSpecialEventReached; // EventListener.cs  
    public event Action OnStardateChanged;
    private float timer; 
    private bool showTime = false; // set true in MainMenuUIController
    public int currentStardate { get; private set; }

    private Coroutine timeCoroutine;
    private float timeSpeedMultiplier = 10f;
    public List<TrekEventSO> specialEvents;

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
        timer = timeSpeedMultiplier;
        // Start time progression coroutine
        timeCoroutine = StartCoroutine(TimeProgression());
        currentStardate = 1010;
    }

    void Update()
    {

        if (showTime) // pause time
        {
            timer -= Time.deltaTime;//count down
            OnStardateChanged?.Invoke();
            if (timer <= 0)
            {
                //??Should this stuff be inside TimeProgression???
                currentStardate++;
                timer = timeSpeedMultiplier;
            }
        }
    }

        private System.Collections.IEnumerator TimeProgression()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f / timeSpeedMultiplier); // 10 seconds in game = 1 stardate

            // Check for special events
            CheckSpecialEvents();
        }
    }

    // Check for special events and trigger corresponding actions
    private void CheckSpecialEvents()
    {
        foreach (var specialEvent in specialEvents)
        {
            if (currentStardate == specialEvent.stardate)
            {
                // Trigger special event
                OnSpecialEventReached?.Invoke(specialEvent);
            }
        }
    }

    // Method to set time speed multiplier
    public void SetTimeSpeedMultiplier(float multiplier)
    {
        timeSpeedMultiplier = multiplier;

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
        showTime = false;
    }

    // Method to resume time progression
    public void ResumeTime()
    {
        timeCoroutine = StartCoroutine(TimeProgression());
        showTime = true;
    }

    // Method to get current stardate
    public int GetCurrentDay()
    {
        return currentStardate;
    }
}

