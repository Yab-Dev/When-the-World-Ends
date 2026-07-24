using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectorUI : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private TMPro.TMP_Text selectLabelText;
    [SerializeField] private RectTransform stationedUnitsContent;
    [SerializeField] private RectTransform selectedUnitsContent;

    [Header("Prefabs")]
    [SerializeField] private GameObject stationedUnitObject;
    [SerializeField] private GameObject selectedUnitObject;
    [SerializeField] private GameObject unitInfoWindow;

    private int selectedCap;
    private List<Unit> stationedUnits = new List<Unit>();
    private List<Unit> selectedUnits = new List<Unit>();



    public void InitializeUnitSelector(List<Unit> _stationedUnits, int _selectedCap)
    {
        selectedCap = _selectedCap;
        stationedUnits.AddRange(_stationedUnits);

        UpdateSelectorUI();
    }

    public void UpdateSelectorUI()
    {
        selectLabelText.text = $"Select Units (Up to {selectedCap}):";

        foreach (Transform child in stationedUnitsContent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in selectedUnitsContent)
        {
            Destroy(child.gameObject);
        }

        Dictionary<Unit, StationedUnitUI> uniqueUnits = new Dictionary<Unit, StationedUnitUI>();
        foreach (Unit unit in stationedUnits)
        {
            if (uniqueUnits.ContainsKey(unit)) 
            {
                uniqueUnits[unit].AddUnit();
                continue;
            }

            StationedUnitUI stationedUnitUI = Instantiate(stationedUnitObject, stationedUnitsContent).GetComponent<StationedUnitUI>();
            stationedUnitUI.SetUnit(unit);
            stationedUnitUI.selectUnitButton.onClick.AddListener(() => { SelectUnit(unit); });
            stationedUnitUI.infoButton.onClick.AddListener(() =>
            {
                Window infoWindow = WindowManager.Instance.CreateWindow($"Info: {unit.name}", unitInfoWindow, Vector2.zero);
                infoWindow.GetWindowContent().GetComponent<UnitInfoWindow>().SetInfo(unit);
            });

            uniqueUnits.Add(unit, stationedUnitUI);
        }

        foreach (Unit unit in selectedUnits)
        {
            SelectedUnitUI selectedUnitUI = Instantiate(selectedUnitObject, selectedUnitsContent).GetComponent<SelectedUnitUI>();
            selectedUnitUI.SetUnit(unit);
            selectedUnitUI.stationUnitButton.onClick.AddListener(() => { DeselectUnit(unit); });
        }

        Canvas.ForceUpdateCanvases();
    }

    private void SelectUnit(Unit _unit)
    {
        if (selectedUnits.Count >= selectedCap) { return; }

        stationedUnits.Remove(_unit);
        selectedUnits.Add(_unit);

        UpdateSelectorUI();
    }

    private void DeselectUnit(Unit _unit)
    {
        selectedUnits.Remove(_unit);
        stationedUnits.Add(_unit);

        UpdateSelectorUI();
    }

    public List<Unit> GetSelectedUnits()
    {
        return selectedUnits;
    }
}
