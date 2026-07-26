using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleUnit
{
    public Unit baseUnit;
    public BattleStratagem battleStratagem;

    private int health;
    private int Health 
    { 
        get 
        { 
            return health; 
        } 
        set
        {
            health = value;
            OnBattleUnitHealthChange?.Invoke(health, baseUnit.health);
        } 
    }

    private float attackTimer;
    public float AttackTimer
    {
        get
        {
            return attackTimer;
        }
        set
        {
            attackTimer = value;
            OnBattleUnitAttackTimerChange?.Invoke(attackTimer, baseUnit.speed);
        }
    }
    private BattleUnit target;
    public bool isDead;

    private bool isEnemy;
    private GameBattle gameBattle;

    public delegate void BattleUnitUpdate();
    public event BattleUnitUpdate OnBattleUnitDeath;

    public delegate void BattleUnitStatChange(float newValue, float maxValue);
    public event BattleUnitStatChange OnBattleUnitHealthChange;
    public event BattleUnitStatChange OnBattleUnitAttackTimerChange;

    public BattleUnit(Unit _unit, bool _isEnemy, GameBattle _battle)
    {
        baseUnit = _unit;
        Heal();
        ResetTimer();
        target = null;
        isDead = false;

        isEnemy = _isEnemy;

        gameBattle = _battle;

        battleStratagem = new BattleStratagem(_unit.stratagem, this, gameBattle);
    }

    public BattleUnit SetTarget(List<BattleUnit> _targets)
    {
        List<BattleUnit> flyingUnits = new List<BattleUnit>();
        List<BattleUnit> nonFlyingUnits = new List<BattleUnit>();
        target = null;

        foreach (BattleUnit unit in _targets)
        {
            if (unit.isDead) { continue; }

            if (unit.baseUnit.isFlyingUnit)
            {
                flyingUnits.Add(unit);
            }
            else
            {
                nonFlyingUnits.Add(unit);
            }
        }

        if (flyingUnits.Count == 0 && nonFlyingUnits.Count == 0) { return target; }

        try
        {
            if (baseUnit.targetsFlyingUnits)
            {
                if (flyingUnits.Count == 0)
                {
                    target = nonFlyingUnits[Random.Range(0, nonFlyingUnits.Count)];
                }
                else
                {
                    target = flyingUnits[Random.Range(0, flyingUnits.Count)];
                }
            }
            else
            {
                if (nonFlyingUnits.Count == 0)
                {
                    target = flyingUnits[Random.Range(0, flyingUnits.Count)];
                }
                else
                {
                    target = nonFlyingUnits[Random.Range(0, nonFlyingUnits.Count)];
                }
            }

        }
        catch
        {
            Debug.Log($"CANNOT FIND TARGET FOR SOME REASON: ENEMY:{isEnemy}, TARGETS:{baseUnit.targetsFlyingUnits}");
            Debug.Log($"Flying targets: {flyingUnits.Count}");
            Debug.Log($"Non Flying targets: {nonFlyingUnits.Count}");
        }

        return target;
    }

    public void Tick()
    {
        if (isDead) { return; }

        AttackTimer -= Time.deltaTime * GameManager.Instance.morale;
        OnBattleUnitAttackTimerChange?.Invoke(AttackTimer, baseUnit.speed);

        battleStratagem.Tick();

        if (AttackTimer <= 0.0f)
        {
            Attack();
            ResetTimer();
        }
    }

    public void Attack()
    {
        if (target == null) { return; }

        if (target.isDead)
        {
            SetTarget(gameBattle.GetTargetList(isEnemy));
            if (target == null) { return; }
        }

        target.Damage(baseUnit.damage);
        battleStratagem.UnitAttack();
    }

    public void Damage(int _amount)
    {
        Health -= _amount;
        Health = Mathf.Clamp(Health, 0, baseUnit.health);
        if (Health <= 0)
        {
            isDead = true;
            OnBattleUnitDeath?.Invoke();
        }
    }

    public void Heal()
    {
        Health = baseUnit.health;
    }

    public void ResetTimer()
    {
        AttackTimer = baseUnit.speed;
    }
}
