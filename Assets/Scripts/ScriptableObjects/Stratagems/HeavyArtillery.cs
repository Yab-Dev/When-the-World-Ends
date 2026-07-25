using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stratagem/Heavy Artillery")]
public class HeavyArtillery : Stratagem
{
    [Header("Stats")]
    public int damage;

    public override void Use(GameBattle _gameBattle)
    {
        _gameBattle.enemyUnit.Damage(damage);
    }
}
