using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEncounterSpawnerManager : MonoBehaviour
{
    public static BattleEncounterSpawnerManager Instance;

    [Header("Data")]
    [SerializeField] private float firstSpawnEncounterDelay;
    [SerializeField] private float initialSpawnEncounterDelay;
    [SerializeField] private float lowTimeThreshhold;
    [SerializeField] private float lowTimeSpawnEncounterDelay;
    [SerializeField] private float finalMinuteSpawnEncounterDelay;
    [SerializeField] private List<BattleEvent> earlyGameBattleEvents = new List<BattleEvent>();
    [SerializeField] private float midGameBattleEventStartTime;
    [SerializeField] private List<BattleEvent> midGameBattleEvents = new List<BattleEvent>();
    [SerializeField] private float lateGameBattleEventStartTime;
    [SerializeField] private List<BattleEvent> lateGameBattleEvents = new List<BattleEvent>();

    [Header("Prefabs")]
    [SerializeField] private GameObject mapBattleEncounterObject;

    private float spawnEncounterTimer;

    private bool gameStarted;



    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(this); return; }
    }

    public void StartGame()
    {
        gameStarted = true;

        spawnEncounterTimer = initialSpawnEncounterDelay - firstSpawnEncounterDelay;
    }

    private void Update()
    {
        if (gameStarted)
        {
            spawnEncounterTimer += Time.deltaTime;

            float spawnThreshhold = initialSpawnEncounterDelay;
            if (GameManager.Instance.doomsdayTimer <= 60)
            {
                spawnThreshhold = finalMinuteSpawnEncounterDelay;
            }
            else if (GameManager.Instance.doomsdayTimer <= lowTimeThreshhold)
            {
                spawnThreshhold = lowTimeSpawnEncounterDelay;
            }

            if (spawnEncounterTimer >= spawnThreshhold)
            {
                SpawnBattleEncounter();
                spawnEncounterTimer = 0;
            }
        }
    }

    private void SpawnBattleEncounter()
    {
        List<BattleEncounterLocation> allLocations = new List<BattleEncounterLocation>();
        foreach (WorldZone zone in GameManager.Instance.zones)
        {
            foreach (GameObject location in zone.battleEncounterSpawnLocations)
            {
                allLocations.Add(location.GetComponent<BattleEncounterLocation>());
            }
        }
        BattleEncounterLocation battleLocation = allLocations[Random.Range(0, allLocations.Count)];

        MapBattleEncounter mapBattleEncounter = Instantiate(mapBattleEncounterObject, battleLocation.transform).GetComponent<MapBattleEncounter>();
        mapBattleEncounter.zone = battleLocation.zone;
        mapBattleEncounter.battleEvent = GetBattleEvent();
    }

    private BattleEvent GetBattleEvent()
    {
        List<BattleEvent> setBattleEvents = new List<BattleEvent>();

        if (GameManager.Instance.survivedTime >= lateGameBattleEventStartTime)
        {
            setBattleEvents = lateGameBattleEvents;
        }
        else if (GameManager.Instance.survivedTime >= midGameBattleEventStartTime)
        {
            setBattleEvents = midGameBattleEvents;
        }
        else
        {
            setBattleEvents = earlyGameBattleEvents;
        }

        return setBattleEvents[Random.Range(0, setBattleEvents.Count)];
    }
}
