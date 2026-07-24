using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationedUnitUI : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private Image unitIcon;
    [SerializeField] private TMPro.TMP_Text unitCountText;
    public Button selectUnitButton;
    public Button infoButton;

    private Unit currentUnit;
    private int unitCount;



    public void SetUnit(Unit _unit, int count = 1)
    {
        currentUnit = _unit;
        unitCount = count;
        unitIcon.sprite = _unit.icon;

        unitCountText.text = unitCount.ToString();
    }

    public Unit GetUnit() { return currentUnit; }

    public void AddUnit()
    {
        unitCount++;

        unitCountText.text = unitCount.ToString();
    }

    public void RemoveUnit()
    {
        if (unitCount > 0)
        {
            unitCount--;

            unitCountText.text = unitCount.ToString();
        }
    }
}
