using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnitDisplayUI : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private Image unitIcon;
    [SerializeField] private StatBar unitHealthBar;
    [SerializeField] private StatBar unitAttackTimerBar;



    public void SetIcon(Sprite _icon)
    {
        unitIcon.sprite = _icon;
    }

    public void SetHealthBar(float _value, float _maxValue)
    {
        unitHealthBar.SetStatBar(_value / _maxValue);
    }

    public void SetAttackTimerBar(float _value, float _maxValue)
    {
        unitAttackTimerBar.SetStatBar(1.0f - (_value / _maxValue));
    }
}
