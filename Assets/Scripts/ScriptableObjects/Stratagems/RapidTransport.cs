using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stratagem/RapidTransport")]
public class RapidTransport : Stratagem
{
    [Header("Stats")]
    [SerializeField] private int chargeIncrease;

    public override void Use(GameBattle _gameBattle)
    {
        foreach (BattleUnit battleUnit in _gameBattle.playerUnits)
        {
            if (battleUnit.isDead) { continue; }
            battleUnit.battleStratagem.ChargeAmount += chargeIncrease;
        }
    }
}
