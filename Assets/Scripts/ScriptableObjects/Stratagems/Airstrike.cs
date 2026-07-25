using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stratagem/Airstrike")]
public class Airstrike : Stratagem
{
    [Header("Stats")]
    public int damage;
    public float attackChargeIncrease;

    public override void Use(GameBattle _gameBattle)
    {
        _gameBattle.enemyUnit.Damage(damage);
        foreach (BattleUnit battleUnit in _gameBattle.playerUnits)
        {
            if (battleUnit.isDead) { continue; }
            battleUnit.AttackTimer -= attackChargeIncrease;
        }
    }
}
