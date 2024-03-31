using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Core;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    // Event to trigger when the current day matches a special event
    public event Action<TrekEventSO> OnSpecialEventReached;

    // Current day in the game
    public int currentDay = 1;

    // Coroutine for time progression
    private Coroutine timeCoroutine;

    // Speed multiplier for time progression
    public float timeSpeedMultiplier = 1f;

    // List of special events
    public List<TrekEventSO> specialEvents;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        // Start time progression coroutine
        timeCoroutine = StartCoroutine(TimeProgression());
    }

    // Coroutine for time progression
    private System.Collections.IEnumerator TimeProgression()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f / timeSpeedMultiplier); // 10 seconds in game = 1 day

            // Increment current day
            currentDay++;

            // Check for special events
            CheckSpecialEvents();
        }
    }

    // Check for special events and trigger corresponding actions
    private void CheckSpecialEvents()
    {
        foreach (var specialEvent in specialEvents)
        {
            if (currentDay == specialEvent.day)
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
    }

    // Method to resume time progression
    public void ResumeTime()
    {
        timeCoroutine = StartCoroutine(TimeProgression());
    }

    // Method to get current day
    public int GetCurrentDay()
    {
        return currentDay;
    }
}

