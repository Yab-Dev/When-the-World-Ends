using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stratagem/Charge")]
public class Charge : Stratagem
{
    [Header("Stats")]
    public float attackChargeIncrease;

    public override void Use(GameBattle _gameBattle)
    {
        foreach (BattleUnit battleUnit in _gameBattle.playerUnits)
        {
            if (battleUnit.isDead) { continue; }
            battleUnit.AttackTimer -= attackChargeIncrease;
        }
    }
}
