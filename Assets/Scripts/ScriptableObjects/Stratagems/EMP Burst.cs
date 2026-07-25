using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stratagem/EMP Burst")]
public class EMPBurst : Stratagem
{
    [Header("Stats")]
    public float attackChargeDecrease;

    public override void Use(GameBattle _gameBattle)
    {
        _gameBattle.enemyUnit.AttackTimer += attackChargeDecrease;
    }
}
