using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleStratagem
{
    public Stratagem baseStratagem;
    private BattleUnit battleUnit;
    private GameBattle gameBattle;

    private float chargeAmount;
    public float ChargeAmount
    {
        get
        {
            return chargeAmount;
        }
        set
        {
            chargeAmount = value;
            chargeAmount = Mathf.Clamp(chargeAmount, 0, baseStratagem.chargeMax);
            OnBattleStratagemChargeAmountChange?.Invoke(chargeAmount, baseStratagem.chargeMax);
        }
    }

    public delegate void BattleStratagemStatChange(float newValue, float maxValue);
    public event BattleStratagemStatChange OnBattleStratagemChargeAmountChange;



    public BattleStratagem(Stratagem _baseStratagem, BattleUnit _battleUnit, GameBattle _gameBattle)
    {
        baseStratagem = _baseStratagem;
        battleUnit = _battleUnit;
        gameBattle = _gameBattle;
    }

    public void Tick()
    {
        ChargeAmount += Time.deltaTime * baseStratagem.chargePerAttack;
    }

    public void UnitAttack()
    {
        ChargeAmount += baseStratagem.chargePerAttack;
    }

    public void Use()
    {
        baseStratagem.Use(gameBattle);
        ChargeAmount = 0;
    }
}
