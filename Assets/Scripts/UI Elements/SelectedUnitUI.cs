using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedUnitUI : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private Image unitIcon;
    public Button stationUnitButton;


    public void SetUnit(Unit _unit)
    {
        unitIcon.sprite = _unit.icon;
    }
}
