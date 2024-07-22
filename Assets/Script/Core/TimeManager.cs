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
    //private float minuteToRealTime = 2f;
    private float timer;
    private bool showTime = false;

    //int moveCounter = 5;
    public int currentStardate { get; private set; }
    //public int currentFleetMoves;

    private Coroutine timeCoroutine;
    public float timeSpeedMultiplier = 1f;
    public List<TrekEventSO> specialEvents;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        // Start time progression coroutine
        timeCoroutine = StartCoroutine(TimeProgression());
        currentStardate = 1010;

    }
    private void Start()
    {
        GameManager.Instance.timeManager = this;
        timer = timeSpeedMultiplier;
    }

    void Update()
    {

        if (showTime)
        {
            timer -= Time.deltaTime;
            OnStardateChanged?.Invoke();
            if (timer <= 0)
            {
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

            // Increment current stardate
            currentStardate++;
            OnStardateChanged?.Invoke(); // StardateUIController.OnEnabled() called
            // OnFleetMoves(FleetController)?.Invoke();

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
    }

    // Method to resume time progression
    public void ResumeTime()
    {
        timeCoroutine = StartCoroutine(TimeProgression());
    }

    // Method to get current stardate
    public int GetCurrentDay()
    {
        return currentStardate;
    }
}

