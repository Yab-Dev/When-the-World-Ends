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
    [SerializeField] private List<BattleEvent> battleEvents = new List<BattleEvent>();

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
        
    }
}
