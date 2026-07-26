using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public static RandomEventManager Instance;

    [Header("Data")]
    [SerializeField] private Vector2 timeBetweenEvents;
    [SerializeField] private List<ChoiceEvent> randomEvents = new List<ChoiceEvent>();

    private bool isStarted;
    private float eventTimer;
    private List<ChoiceEvent> spawnedEvents = new List<ChoiceEvent>();



    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(this); return; }
    }

    public void StartRandomEvents()
    {
        isStarted = true;
        SetEventTime();
    }

    private void Update()
    {
        if (isStarted)
        {
            eventTimer -= Time.deltaTime * ((GameManager.Instance.GetTotalInfluence() + 100.0f) / 100.0f);
            if (eventTimer <= 0.0f)
            {
                SpawnEvent();
                SetEventTime();
            }
        }
    }

    private void SetEventTime()
    {
        eventTimer = Random.Range(timeBetweenEvents.x, timeBetweenEvents.y);
    }

    private void SpawnEvent()
    {
        Debug.Log("Spawning Event...");

        ChoiceEvent choiceEvent = null;
        do
        {
            choiceEvent = randomEvents[Random.Range(0, randomEvents.Count)];
        }
        while (choiceEvent.isUnique && spawnedEvents.Contains(choiceEvent));
        spawnedEvents.Add(choiceEvent);

        Debug.Log("Choice event found...");

        WorldZone zone = null;
        do
        {
            zone = GameManager.Instance.zones[Random.Range(0, GameManager.Instance.zones.Count)];
        }
        while (choiceEvent.blackListZones.Contains(zone.name));

        Debug.Log("Zone found...");

        choiceEvent.DisplayEvent(zone);
    }
}
