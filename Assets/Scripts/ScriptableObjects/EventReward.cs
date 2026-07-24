using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventReward
{
    [Header("Rewards")]
    public float timeReward;
    public int influenceReward;
    public float moraleReward;
    public List<Unit> unitReward = new List<Unit>();



    public void RedeemReward(WorldZone _zone)
    {
        GameManager.Instance.doomsdayTimer += timeReward;
        GameManager.Instance.morale += moraleReward;

        _zone.Influence += influenceReward;
        foreach (Unit unit in unitReward)
        {
            _zone.AddUnit(unit);
        }

        WindowManager.Instance.CreateRewardsWindow("Rewards", Vector2.zero, this);
    }
}
