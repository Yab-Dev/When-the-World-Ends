using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStartWindow : MonoBehaviour, IWindowInteract
{
    public static BattleStartWindow Instance;

    [Header("Cache")]
    [SerializeField] private TMPro.TMP_Text enemyInfoText;
    [SerializeField] private UnitSelectorUI unitSelector;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button startButton;

    private Window windowScript;
    private WorldZone zone;
    private BattleEvent battleEvent;

    public delegate void BattleStart();
    public event BattleStart OnBattleStart;


    private void Awake()
    {
        if (Instance != null)
        {
            Instance.windowScript.CloseWindow();
        }

        Instance = this;
    }

    public void InitializeUI(WorldZone _zone, BattleEvent _battleEvent)
    {
        zone = _zone;
        battleEvent = _battleEvent;

        enemyInfoText.text = $"Target: {_battleEvent.enemyUnit.name} (Strength: {_battleEvent.enemyUnit.unitStrength})";

        unitSelector.InitializeUnitSelector(_zone.GetStationedUnits(), 5, _zone);

        exitButton.onClick.AddListener(windowScript.CloseWindow);

        startButton.onClick.AddListener(StartBattle);
    }

    public void SetWindowScript(Window _windowScript)
    {
        windowScript = _windowScript;
    }

    private void StartBattle()
    {
        if (unitSelector.GetSelectedUnits().Count == 0) { return; }

        OnBattleStart?.Invoke();

        GameManager.Instance.StartBattle(battleEvent, unitSelector.GetSelectedUnits(), zone);

        foreach (Unit unit in unitSelector.GetSelectedUnits())
        {
            zone.RemoveUnit(unit);
        }

        windowScript.CloseWindow();
    }
}
