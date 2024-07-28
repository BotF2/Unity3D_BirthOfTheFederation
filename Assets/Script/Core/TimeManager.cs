using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using Assets.Core;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    
    public event Action<TrekEventSO> OnSpecialEventReached; // ?? EventListener subscribes the HandleSpecialEvent(TrekEvenTyep enum) function in OnSpecialEventReached
    public event Action OnStardateChanged; //StardateUIController subscribes the UpdateDateText() function
    private float timer;
    public int currentStardate { get; private set; }

    private Coroutine timeCoroutine;
    private float timeSpeedReducer = 10f;
    //public GameObject specialEventsGO;
    //[SerializeField]
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
        //var specialEvent  = Resources.Load<TrekEventSO>($"Assets/Resources/TrekEventSO/RemoveTempTargets.cs");
        //specialEvents.Add(specialEvent);
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
            yield return new WaitForSeconds(10f / timeSpeedReducer); // 10 seconds in game = 1 stardate

            // Increment current day
            //currentDay++;
            currentStardate++;
            OnStardateChanged?.Invoke();

            // Check for special events
           // CheckSpecialEvents();
        }
        
    }

    // Check for special events and trigger corresponding actions
    private void CheckSpecialEvents()
    {
        foreach (var specialEvent in specialEvents)
        {
            if (specialEvent != null && currentStardate == specialEvent.stardate)
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

