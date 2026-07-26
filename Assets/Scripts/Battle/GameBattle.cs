using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattle : MonoBehaviour, IWindowInteract
{
    [Header("Units")]
    public BattleUnit enemyUnit;
    public List<BattleUnit> playerUnits = new List<BattleUnit>();
    public bool battleActive;

    public Window windowScript;

    private EventReward winReward;
    private EventReward loseReward;

    private WorldZone zone;

    public delegate void GameBattleUpdate();
    public event GameBattleUpdate OnGameBattleStart;
    public event GameBattleUpdate OnGameBattleWin;
    public event GameBattleUpdate OnGameBattleLose;

    public static event TutorialManager.TutorialNotify OnBattleStarted;
    public static event TutorialManager.TutorialNotify OnBattleEnded;



    private void Awake()
    {
        battleActive = false;
    }

    private void Update()
    {
        if (battleActive)
        {
            enemyUnit.Tick();
            foreach (BattleUnit unit in playerUnits)
            {
                unit.Tick();
            }
        }
    }

    public void SetEnemyUnit(Unit _enemyUnit)
    {
        enemyUnit = new BattleUnit(_enemyUnit, true, this);
        enemyUnit.OnBattleUnitDeath += CheckForPlayerWin;
    }

    public void AddPlayerUnit(Unit _playerUnit)
    {
        if (playerUnits.Count >= 5) { return; }

        BattleUnit playerUnit = new BattleUnit(_playerUnit, false, this);
        playerUnit.OnBattleUnitDeath += CheckForEnemyWin;
        playerUnits.Add(playerUnit);
    }

    public void SetRewards(EventReward _winReward, EventReward _loseReward)
    {
        winReward = _winReward;
        loseReward = _loseReward;
    }

    public void SetZone(WorldZone _zone)
    {
        zone = _zone;
    }

    public void StartBattle()
    {
        if (enemyUnit == null) { return; }
        if (playerUnits.Count == 0) { return; }

        enemyUnit.SetTarget(GetTargetList(true));
        foreach (BattleUnit unit in playerUnits)
        {
            unit.SetTarget(GetTargetList(false));
        }

        OnGameBattleStart?.Invoke();

        enemyUnit.Heal();
        enemyUnit.ResetTimer();
        foreach (BattleUnit unit in playerUnits)
        {
            unit.Heal();
            unit.ResetTimer();
        }

        battleActive = true;
        OnBattleStarted?.Invoke();
    }

    public List<BattleUnit> GetTargetList(bool _isEnemy)
    {
        List<BattleUnit> targetList = new List<BattleUnit>();

        if (_isEnemy)
        {
            targetList.AddRange(playerUnits);
        }
        else
        {
            targetList.Add(enemyUnit);
        }

        return targetList;
    }

    private void CheckForPlayerWin()
    {
        if (enemyUnit.isDead)
        {
            battleActive = false;

            foreach (BattleUnit unit in playerUnits)
            {
                if (unit.isDead) { continue; }
                zone.AddUnit(unit.baseUnit);
            }

            winReward.RedeemReward(zone);

            OnGameBattleWin?.Invoke();
            OnBattleEnded?.Invoke();
        }
    }

    private void CheckForEnemyWin()
    {
        foreach (BattleUnit unit in playerUnits)
        {
            if (!unit.isDead) { return; }
        }

        battleActive = false;

        loseReward.RedeemReward(zone);

        OnGameBattleLose?.Invoke();
        OnBattleEnded?.Invoke();
    }

    public void SetWindowScript(Window _windowScript)
    {
        windowScript = _windowScript;
    }
}
