using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stratagem : ScriptableObject
{
    [Header("Data")]
    public new string name;
    public string description;

    [Header("Visuals")]
    public Sprite icon;

    [Header("Stats")]
    public float chargeMax;
    public float chargePerSecond;
    public float chargePerAttack;



    public abstract void Use(GameBattle _gameBattle);
}
