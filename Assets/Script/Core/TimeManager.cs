using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Core;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    // Event to trigger when the current stardate matches a special event
    public event Action<TrekEventSO> OnSpecialEventReached;
    // Event as time passes
    //public event Action<TrekEventSO> OpenFleetUI;
    // Event delta Stardate
    public event Action OnStardateChanged;
   // public event Action<FleetController> OnFleetMoves;
    private float minuteToRealTime = 2f;
    private float timer;
    private bool showTime = false;

    int moveCounter = 5;
    // Current stardate in the game
    public int currentStardate { get; private set; }
    public int currentFleetMoves;
    //public static int gameMinute { get; private set; }
    //public static int starDate { get; private set; }
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
        currentStardate = 1010;
        //timer = ;
    }
    private void Start()
    {
        GameManager.Instance.timeManager = this;
    }
    void Update()
    {

        if (showTime)
        {
            timer -= Time.deltaTime;
            if (moveCounter < 1)
            {
                //for (int i = 0; i < GalaxyView._movingGalaxyObjects.Count; i++)
                //{
                //    MoveFleets myMoveGalactic = GalaxyView._movingGalaxyObjects[i].GetComponent<MoveFleets>();
                //    myMoveGalactic.ThrustVector(); // move galaxy objects
                //                                   // myMoveGalactic.MovePlanePoint(); // move objects plane endpoints
                //}
                //moveCounter = 5;
            }
            else moveCounter--;
            if (timer <= 0)
            {
                //gameMinute++;
                //OnMinuteChanged?.Invoke();
                //if (gameMinute >= 99)
                //{
                //    stardate++;
                //    gameMinute = 0;
                //    OnStardateChanged?.Invoke();
                //}
                //timer = minuteToRealTime;
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
            OnStardateChanged?.Invoke();
            //OnFleetMoves(FleetController)?.Invoke();

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

