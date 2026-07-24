using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class WorldZoneInfoWindow : MonoBehaviour, IWindowInteract
{
    public static WorldZoneInfoWindow Instance;

    [Header("Cache")]
    [SerializeField] private TMPro.TMP_Text influenceText;
    [SerializeField] private TMPro.TMP_Text generatedUnitText;
    [SerializeField] private StatBar generatedUnitBar;
    [SerializeField] private UnitSelectorUI unitSelector;
    [SerializeField] private Button exitButton;
    [SerializeField] private TMPro.TMP_Dropdown unitDestinationDropdown;
    [SerializeField] private Button moveUnitsButton;

    [Header("Prefab")]
    [SerializeField] private GameObject unitTransferWindow;

    private Window windowScript;
    private WorldZone zone;



    private void Awake()
    {
        if (Instance != null)
        {
            Instance.windowScript.CloseWindow();
        }

        Instance = this;
    }

    private void OnDisable()
    {
        zone.OnWorldZoneUnitLoadUpdate -= SetLoadingBar;
    }

    public void InitializeUI(WorldZone _zone)
    {
        zone = _zone;

        SetInfluenceText(_zone.Influence);
        _zone.OnWorldZoneInfluenceUpdate += SetInfluenceText;

        generatedUnitText.text = $"Generated Unit: {_zone.generatedUnit.name}";
        _zone.OnWorldZoneUnitLoadUpdate += SetLoadingBar;

        unitSelector.InitializeUnitSelector(_zone.GetStationedUnits(), 8, _zone);

        exitButton.onClick.AddListener(windowScript.CloseWindow);

        unitDestinationDropdown.ClearOptions();
        List<string> connectedZones = new List<string>();
        foreach (WorldZone worldZone in _zone.connectedZones)
        {
            connectedZones.Add(worldZone.name);
        }
        unitDestinationDropdown.AddOptions(connectedZones);

        moveUnitsButton.onClick.AddListener(MoveUnits);
    }

    public void SetWindowScript(Window _windowScript)
    {
        windowScript = _windowScript;
    }

    private void MoveUnits()
    {
        if (unitSelector.GetSelectedUnits().Count == 0) { return; }

        WorldZone destinationZone = null;
        foreach (WorldZone worldZone in zone.connectedZones)
        {
            if (worldZone.name == unitDestinationDropdown.options[unitDestinationDropdown.value].text)
            {
                destinationZone = worldZone; break;
            }
        }
        if (destinationZone == null) { return; }

        Window transferWindow = WindowManager.Instance.CreateWindow("Transferring Units", unitTransferWindow, Vector2.zero);
        transferWindow.GetWindowContent().GetComponent<UnitTransferWindow>().BeginLoadingBar(unitSelector.GetSelectedUnits(), destinationZone, 3.0f);
        foreach (Unit unit in unitSelector.GetSelectedUnits())
        {
            zone.RemoveUnit(unit);
        }

        windowScript.CloseWindow();
    }

    private void SetInfluenceText(int _influence)
    {
        influenceText.text = $"Influence: {_influence}";
    }

    private void SetLoadingBar(float _value, float _maxValue)
    {
        generatedUnitBar.SetStatBar(_value / _maxValue);
    }
}
