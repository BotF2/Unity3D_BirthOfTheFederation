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
    private bool runClock = false;
    public int currentStardate { get; private set; }

    private Coroutine timeCoroutine;
    private float timeSpeedReducer = 10f;
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
        timer = timeSpeedReducer;
        // Start time progression coroutine
        timeCoroutine = StartCoroutine(TimeProgression());
        currentStardate = 1010;
        runClock = true;
    }

    void Update()
    {
        //if (MainMenuUIController.instance.PastMainMenu) // pause time
        //{
        //    //timeCoroutine = StartCoroutine(TimeProgression());
        //    ////runClock = true;
        //    //timer -= Time.deltaTime;//count down
        //    ////OnStardateChanged?.Invoke();
        //    //if (timer <= 0)
        //    //{
        //    //    //??Should this stuff be inside TimeProgression???
        //    //    currentStardate++;
        //    //    //OnStardateChanged?.Invoke();
        //    //    //CheckSpecialEvents();
        //    //    timer = timeSpeedReducer;
        //    //}
        //}
    }

        private System.Collections.IEnumerator TimeProgression()
        {

            while (MainMenuUIController.instance.PastMainMenu && runClock)
            {
                yield return new WaitForSeconds(10f / timeSpeedReducer); // 10 seconds in game = 1 stardate

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
        runClock = false;
    }

    // Method to resume time progression
    public void ResumeTime()
    {
        timeCoroutine = StartCoroutine(TimeProgression());
        runClock = true;
    }

    // Method to get current stardate
    public int GetCurrentDay()
    {
        return currentStardate;
    }
}

