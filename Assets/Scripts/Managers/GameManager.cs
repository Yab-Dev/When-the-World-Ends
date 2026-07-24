using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Gameplay Data")]
    [SerializeField] public float doomsdayTimer;

    [Header("Prefabs")]
    [SerializeField] private GameObject testWindowContent;
    [SerializeField] private GameObject battleWindowContent;
    [SerializeField] private GameObject doomsdayTimerWindowContent;
    [SerializeField] private Unit enemyUnit;
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

    public void StartBattle(Unit _enemyUnit, List<Unit> _playerUnits)
    {
        Window battleWindow = WindowManager.Instance.CreateWindow("Battle Network", battleWindowContent, Vector2.zero);
        GameBattle gameBattle = battleWindow.GetWindowContent().GetComponent<GameBattle>();

        gameBattle.SetEnemyUnit(_enemyUnit);
        foreach (Unit _playerUnit in _playerUnits)
        {
            gameBattle.AddPlayerUnit(_playerUnit);
        }

        gameBattle.StartBattle();
    }

    public void StartGame()
    {
        isGameStarted = true;
        WindowManager.Instance.CreateWindow("When the World Ends", testWindowContent, Vector2.zero);
        WindowManager.Instance.CreateWindow("Doomsday Clock", doomsdayTimerWindowContent, new Vector2(0, 80));
        StartBattle(enemyUnit, playerUnits);
    }
}
