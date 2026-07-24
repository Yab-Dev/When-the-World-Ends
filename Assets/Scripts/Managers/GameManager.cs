using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Gameplay Data")]
    [SerializeField] public float doomsdayTimer;
    [SerializeField] public float morale;
    [SerializeField] public List<WorldZone> zones = new List<WorldZone>();

    [Header("Cache")]
    [SerializeField] private GameObject border;
    [SerializeField] private GameObject worldMap;

    [Header("Prefabs")]
    [SerializeField] private GameObject mainMenuWindowContent;
    [SerializeField] private GameObject battleWindowContent;
    [SerializeField] private GameObject doomsdayTimerWindowContent;
    [SerializeField] private GameObject playerStatsWindowContent;

    private bool isGameStarted = false;



    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(this); return; }
    }

    private void Start()
    {
        border.SetActive(false);
        worldMap.SetActive(false);

        StartCoroutine(IntroAnimation());
    }

    private void Update()
    {
        if (isGameStarted)
        {
            doomsdayTimer -= Time.deltaTime;
        }
    }

    public void StartBattle(BattleEvent _battleEvent, List<Unit> _playerUnits, WorldZone _zone)
    {
        Window battleWindow = WindowManager.Instance.CreateWindow("Battle Network", battleWindowContent, Vector2.zero);
        GameBattle gameBattle = battleWindow.GetWindowContent().GetComponent<GameBattle>();

        gameBattle.SetEnemyUnit(_battleEvent.enemyUnit);
        foreach (Unit _playerUnit in _playerUnits)
        {
            gameBattle.AddPlayerUnit(_playerUnit);
        }

        gameBattle.SetRewards(_battleEvent.winReward, _battleEvent.loseReward);
        gameBattle.SetZone(_zone);

        gameBattle.StartBattle();
    }

    public IEnumerator IntroAnimation()
    {
        yield return new WaitForSeconds(1.0f);

        border.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        WindowManager.Instance.CreateWindow("When the World Ends", mainMenuWindowContent, Vector2.zero);
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2.0f);

        WindowManager.Instance.CreateWindow("Stats", playerStatsWindowContent, new Vector2(-170, 100));
        yield return new WaitForSeconds(1.0f);

        isGameStarted = true;
        WindowManager.Instance.CreateWindow("Doomsday Clock", doomsdayTimerWindowContent, new Vector2(0, 80));

        foreach (WorldZone zone in zones)
        {
            zone.StartGame();
        }

        yield return new WaitForSeconds(1.0f);

        worldMap.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        // Start of game radio event
    }

    public int GetTotalInfluence()
    {
        int total = 0;
        foreach (WorldZone zone in zones)
        {
            total += zone.Influence;
        }
        return total;
    }
}
