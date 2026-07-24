using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/BattleEvent")]
public class BattleEvent : Event
{
    [Header("Battle Event Data")]
    public Unit enemyUnit;
    public EventReward winReward;
    public EventReward loseReward;
}
