using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stratagem/BurstFire")]
public class BurstFire : Stratagem
{
    [Header("Stats")]
    public int damage;

    public override void Use(GameBattle _gameBattle)
    {
        _gameBattle.enemyUnit.Damage(damage);
    }
}
