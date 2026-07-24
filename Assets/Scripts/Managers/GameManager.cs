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

    [Header("Prefabs")]
    [SerializeField] private GameObject testWindowContent;
    [SerializeField] private GameObject battleWindowContent;
    [SerializeField] private GameObject doomsdayTimerWindowContent;
    [SerializeField] private GameObject playerStatsWindowContent;
    [SerializeField] private BattleEvent testBattle;
    [SerializeField] private List<Unit> playerUnits = new List<Unit>();

    private bool isGameStarted = false;



    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(this); return; }
    }

    private void Start()
    {
        StartGame();
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

    public void StartGame()
    {
        isGameStarted = true;

        WindowManager.Instance.CreateWindow("When the World Ends", testWindowContent, Vector2.zero);
        WindowManager.Instance.CreateWindow("Doomsday Clock", doomsdayTimerWindowContent, new Vector2(0, 80));
        WindowManager.Instance.CreateWindow("Stats", playerStatsWindowContent, new Vector2(-170, 100));
        StartBattle(testBattle, playerUnits, zones[0]);

        foreach (WorldZone zone in zones)
        {
            zone.StartGame();
        }
    }

    public int GetTotalInfluence()
    {
        int total = 0;
        foreach (WorldZone zone in zones)
        {
            total += zone.influence;
        }
        return total;
    }
}
