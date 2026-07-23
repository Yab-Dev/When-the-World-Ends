using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Unit : ScriptableObject
{
    [Header("Data")]
    public new string name;

    [Header("Visuals")]
    public Sprite icon;

    [Header("Stats")]
    public int health;
    public int damage;
    public float speed;
}
